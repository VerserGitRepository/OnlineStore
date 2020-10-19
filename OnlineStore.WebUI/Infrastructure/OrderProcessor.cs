using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Net;
using System.IO;
using OnlineStore.WebUI.Models;
using OnlineStore.WebUI.ApplicationData;

namespace OnlineStore.WebUI.Infrastructure
{
    public class OrderProcessor
    {
        private ApplicationData.ApplicationData context;
        private Cart cart;
        private Order order;

        //public OrderProcessor(ApplicationData.ApplicationData context, Cart cart, ShippingDetailsViewModel shippingDetails)
        //{
        //    this.context = context;
        //    this.cart = cart;
        //    this.order = new Order
        //    {
        //        OrderDate = DateTime.Now,
        //        Email = shippingDetails.Email,
        //        Sale = context.Sales.Where(x => x.Id == cart.SaleId).First(),
        //        ShipTo = shippingDetails.FirstName + " " + shippingDetails.LastName,
        //        ShippingAddressLine1 = shippingDetails.AddressLine1,
        //        ShippingAddressLine2 = shippingDetails.AddressLine2,
        //        Suburb = shippingDetails.Suburb,
        //        State = shippingDetails.State,
        //        Postcode = shippingDetails.Postcode
        //    };
        //}

        public string ProcessOrder()
        {
            string handOffUrl = string.Empty;
            try
            {
                context.AddToOrders(this.order);
                this.order.Sale.Orders.Add(this.order);
                context.AddLink(this.order.Sale, "Orders", this.order);

                foreach (var l in this.cart.Lines)
                {
                    var orderDetail = new OrderDetail { Quantity = l.Quantity };
                    context.AddToOrderDetails(orderDetail);
                    context.AddLink(this.order, "OrderDetails", orderDetail);

                    orderDetail.SaleProduct = context.SaleProducts.Where(x => x.Id == l.SaleProduct.Id).First();
                    orderDetail.SaleProduct.OrderDetails.Add(orderDetail);
                    context.AddLink(orderDetail.SaleProduct, "OrderDetails", orderDetail);

                    //if (l.AssetAllocation != null)
                    //{
                    //    var assetAllocation = context.AssetAllocations.Where(x => x.Id == l.AssetAllocation.Id).Single();

                    //    assetAllocation.Order = this.order;
                    //    this.order.AssetAllocation.Add(l.AssetAllocation);
                    //    context.AddLink(this.order, "AssetAllocation", assetAllocation);
                    //}
                }

                context.SaveChanges(System.Data.Services.Client.SaveChangesOptions.Batch);

                string tokenRequest = BuildTokenRequest();

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                        WebConfigurationManager.AppSettings["payWayBaseUrl"] + "RequestToken");
                request.KeepAlive = false;
                request.Method = "POST";
                request.Timeout = 60000;
                request.ContentType = "application/x-www-form-urlencoded; charset=" +
                    System.Text.Encoding.UTF8.WebName;

                byte[] requestBody = System.Text.Encoding.UTF8.GetBytes(
                    tokenRequest);

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(requestBody, 0, requestBody.Length);
                requestStream.Close();
                requestStream = null;

                WebResponse response = request.GetResponse();

                Stream responseStream = response.GetResponseStream();
                StreamReader responseReader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                string tokenResponse = responseReader.ReadToEnd();
                responseStream.Close();

                string[] responseParameters = tokenResponse.Split(new Char[] { '&' });
                string token = null;
                for (int i = 0; i < responseParameters.Length; i++)
                {
                    string responseParameter = responseParameters[i];
                    string[] paramNameValue = responseParameter.Split(new Char[] { '=' }, 2);
                    if ("token".Equals(paramNameValue[0]))
                    {
                        token = paramNameValue[1];
                    }
                    else if ("error".Equals(paramNameValue[0]))
                    {
                        LogService.Error(paramNameValue[1]);
                        throw new Exception(paramNameValue[1]);
                    }
                }

                handOffUrl = WebConfigurationManager.AppSettings["payWayBaseUrl"] + "MakePayment";
                handOffUrl += "?biller_code=" + HttpUtility.UrlEncode(WebConfigurationManager.AppSettings["billerCode"]) +
                 "&token=" + HttpUtility.UrlEncode(token);
                this.cart.Clear();
                LogService.info(this.order.OrderNo + "Order processed ");


                return handOffUrl;

            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message + ex.InnerException.StackTrace);
                return handOffUrl;
            }
        }

        private static string BuildTokenRequest()
        {
            StringBuilder tokenRequest = new StringBuilder();
            tokenRequest.Append("username=");
            tokenRequest.Append(WebConfigurationManager.AppSettings["payway_username"]);
            tokenRequest.Append("&password=");
            tokenRequest.Append(WebConfigurationManager.AppSettings["payway_password"]);
            tokenRequest.Append("&biller_code=");
            tokenRequest.Append(WebConfigurationManager.AppSettings["billerCode"]);
            tokenRequest.Append("&merchant_id=");
            tokenRequest.Append(WebConfigurationManager.AppSettings["merchantId"]);
            tokenRequest.Append("&payment_reference=");
     //  tokenRequest.Append(order.OrderNo);
            tokenRequest.Append("&payment_reference_change=false");
            tokenRequest.Append("&surcharge_rates=");
            tokenRequest.Append(HttpUtility.UrlEncode("VI/MC=1.0,AX=1.0,DC=1.0"));
            tokenRequest.Append("&receipt_address=");
           // tokenRequest.Append(HttpUtility.UrlEncode(this.order.Email));
            //foreach (var l in this.cart.Lines)
            //{
            //    tokenRequest.Append("&");
            //    tokenRequest.Append(HttpUtility.UrlEncode(l.SaleProduct.Product.ProductName));
            //    tokenRequest.Append("=");
            //    tokenRequest.Append(HttpUtility.UrlEncode(string.Format("{0},{1}", l.Quantity, l.SaleProduct.PriceIncGST)));
            //}
            LogService.info("Payment URL" + tokenRequest.ToString());
            return tokenRequest.ToString();
        }

        private static string BuildTokenRequestForOnlineSaleOrder(ShippingDetailsViewModel _checkoutDataModel)
        {
            StringBuilder tokenRequest = new StringBuilder();
            tokenRequest.Append("username=");
            tokenRequest.Append(WebConfigurationManager.AppSettings["payway_username"]);
            tokenRequest.Append("&password=");
            tokenRequest.Append(WebConfigurationManager.AppSettings["payway_password"]);
            tokenRequest.Append("&biller_code=");
            tokenRequest.Append(WebConfigurationManager.AppSettings["billerCode"]);
            tokenRequest.Append("&merchant_id=");
            tokenRequest.Append(WebConfigurationManager.AppSettings["merchantId"]);
            tokenRequest.Append("&payment_reference=");
             tokenRequest.Append(_checkoutDataModel.payment_OrderNo);
            tokenRequest.Append("&payment_reference_change=false");
            tokenRequest.Append("&surcharge_rates=");
            tokenRequest.Append(HttpUtility.UrlEncode("VI/MC=1.0,AX=1.0,DC=1.0"));
            tokenRequest.Append("&receipt_address=");
            tokenRequest.Append(HttpUtility.UrlEncode(_checkoutDataModel.Email));

            ShoppingCart item = (ShoppingCart)HttpContext.Current.Session["Productcart"];
            if (item != null)
            {
                foreach (var l in item.Lines)
                {
                    tokenRequest.Append("&");
                    tokenRequest.Append(HttpUtility.UrlEncode(l.SaleProduct.ProductName));
                    tokenRequest.Append("=");
                    tokenRequest.Append(HttpUtility.UrlEncode(string.Format("{0},{1}", 1, l.SaleProduct.PriceIncGST)));
                }
            }
            LogService.info("Payment URL" + tokenRequest.ToString());
            return tokenRequest.ToString();
        }

        public static string ProcessOnlineSaleOrder(ShippingDetailsViewModel _checkoutDataModel)
        {
            string handOffUrl = string.Empty;
            try
            {
            
            string tokenRequest = BuildTokenRequestForOnlineSaleOrder(_checkoutDataModel);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
                    WebConfigurationManager.AppSettings["payWayBaseUrl"] + "RequestToken");
            request.KeepAlive = false;
            request.Method = "POST";
            request.Timeout = 60000;
            request.ContentType = "application/x-www-form-urlencoded; charset=" +
                System.Text.Encoding.UTF8.WebName;

            byte[] requestBody = System.Text.Encoding.UTF8.GetBytes(tokenRequest);

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(requestBody, 0, requestBody.Length);
            requestStream.Close();
            requestStream = null;
            WebResponse response = request.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader responseReader = new StreamReader(responseStream, System.Text.Encoding.UTF8);
            string tokenResponse = responseReader.ReadToEnd();
            responseStream.Close();

            string[] responseParameters = tokenResponse.Split(new Char[] { '&' });
            string token = null;
            for (int i = 0; i < responseParameters.Length; i++)
            {
                string responseParameter = responseParameters[i];
                string[] paramNameValue = responseParameter.Split(new Char[] { '=' }, 2);
                if ("token".Equals(paramNameValue[0]))
                {
                    token = paramNameValue[1];
                }
                else if ("error".Equals(paramNameValue[0]))
                {
                    LogService.Error(paramNameValue[1]);
                    throw new Exception(paramNameValue[1]);
                }
            }
            handOffUrl = WebConfigurationManager.AppSettings["payWayBaseUrl"] + "MakePayment";
            handOffUrl += "?biller_code=" + HttpUtility.UrlEncode(WebConfigurationManager.AppSettings["billerCode"]) +
             "&token=" + HttpUtility.UrlEncode(token);
                //this.cart.Clear();
           // LogService.info(this.order.OrderNo + "Order processed ");
            return handOffUrl;
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message + ex.InnerException.StackTrace);
                return handOffUrl;
            }
        }  
}
}