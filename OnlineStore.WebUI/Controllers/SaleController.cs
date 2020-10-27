using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Net;
using System.Web.Configuration;
using OnlineStore.WebUI.Models;
using OnlineStore.WebUI.ApplicationData;
using OnlineStore.WebUI.Infrastructure;

namespace OnlineStore.WebUI.Controllers
{
    public class SaleController : Controller
    {
        private ApplicationData.ApplicationData applicationDataContext;
        private WMSData.WMSData wmsDataContext;

        public SaleController()
        {
            string username = WebConfigurationManager.AppSettings["Username"];
            string password = WebConfigurationManager.AppSettings["Password"];

            NetworkCredential credentials = new NetworkCredential(username, password);

            if(applicationDataContext == null)
            {
                Uri applicationDataUri = new Uri(WebConfigurationManager.AppSettings["ApplicationDataUri"]);
                applicationDataContext = new ApplicationData.ApplicationData(applicationDataUri);
                applicationDataContext.Credentials = credentials;
            }

            if (wmsDataContext == null)
            {
                Uri wmsDataUri = new Uri(WebConfigurationManager.AppSettings["WMSDataUri"]);
                wmsDataContext = new WMSData.WMSData(wmsDataUri);
                wmsDataContext.Credentials = credentials;
            }
        }

        public ActionResult List(string itemType, string customerCode, string saleId)
        {
            var sale = applicationDataContext.Sales.Where(x => x.SaleID == saleId).Single();

            string employeeId = string.Empty;

            if (Session["EmployeeID"] != null)
                employeeId = Session["EmployeeID"].ToString();

            decimal? itemTypeId = null;

            if(!String.IsNullOrEmpty(itemType))
            {
                var i = wmsDataContext.ItemTypes.Where(x => x.Description == itemType).SingleOrDefault();

                if(i != null)
                {
                    itemTypeId = i.ID;
                }
            }

            List<SaleProductInfo> saleProductInfos;

            if (!string.IsNullOrEmpty(employeeId) && sale.SaleType == "Pre-Allocated")
            {
                saleProductInfos = applicationDataContext.AssetAllocations.Expand("Sale, SaleProduct, SaleProduct/Product, Order").Where(x => x.Sale.SaleID == saleId && x.EmployeeID == employeeId && x.Order == null).ToList().Select(x => new SaleProductInfo { SaleProduct = x.SaleProduct, AssetAllocation = x, InStock = true }).ToList();
            }
            else
            {
                saleProductInfos = applicationDataContext.SaleProducts.Expand("Product").Where(x => x.Sale.SaleID == saleId).ToList().Select(x => new SaleProductInfo { SaleProduct = x, InStock = x.QtyAvailable > 0 }).ToList();
            }

            if(itemTypeId.HasValue)
            {
                saleProductInfos = saleProductInfos.Where(x => x.SaleProduct.Product.ItemType_ID == itemTypeId).ToList();
            }

            var customer = applicationDataContext.Customers.Where(x => x.CustomerCode == customerCode).SingleOrDefault();

            if(customer != null)
            {
                Session["CustomerName"] = customer.CustomerName;
                Session["CustomerCode"] = customer.CustomerCode;
                Session["SaleID"] = saleId;
            }
            ProductsListViewModel model = new ProductsListViewModel {
                SaleProductInfos = saleProductInfos.AsEnumerable(),
                SelectedItemType = itemType,
                Customer = customer,
                SaleId = saleId
            };
            return View(model);
        }
        public ActionResult GetProductImage(int productId)
        {
            string encoding = "image/jpg";
            WebImage image = new WebImage("~/Content/Images/no_product_image.jpg");

            var product = applicationDataContext.Products.Where(x => x.Id == productId).SingleOrDefault();

            if (product != null)
            {
                var productImage = product.ProductImage;

                if (productImage != null && productImage.Length > 0)
                {
                    image = new WebImage(productImage);

                    switch (image.ImageFormat)
                    {
                        case "jpeg":
                            encoding = "image/jpg";
                            break;
                        case "png":
                            encoding = "image/png";
                            break;
                    }
                }
            }

            return File(image.GetBytes(), encoding);
        }

        [HttpPost]
        public ActionResult PaymentReceipt()
        {
            try
            {
                string username = Request.Form["username"];
                string password = Request.Form["password"];
                LogService.info(username + password + "PaymentReceipt Request Form Data");
                int orderNo = Convert.ToInt32(Request.Form["payment_reference"]);
                decimal paymentAmount = Convert.ToDecimal(Request.Form["am_payment"]);
                string cardType = Request.Form["nm_card_scheme"];
                string nameOnCard = Request.Form["nm_card_holder"];
                string truncatedCardNumber = Request.Form["TruncatedCardNumber"];
                int paymentStatus = Convert.ToInt32(Request.Form["fl_success"]);
                if (username != WebConfigurationManager.AppSettings["payway_username"] || password != WebConfigurationManager.AppSettings["payway_password"])
                {
                    LogService.Error(username + password + "Not Matched");
                    return new HttpUnauthorizedResult();
                }
                var order = applicationDataContext.Orders.Where(x => x.OrderNo == orderNo).Single();
                Payment payment = new Payment
                {
                    Order = order,
                    PaymentAmount = paymentAmount,
                    CardType = cardType,
                    NameOnCard = nameOnCard,
                    TruncatedCardNo = truncatedCardNumber,
                    PaymentStatus = paymentStatus
                };
                applicationDataContext.AddToPayments(payment);
                order.Payments.Add(payment);
                applicationDataContext.AddLink(order, "Payments", payment);
                applicationDataContext.SaveChanges(System.Data.Services.Client.SaveChangesOptions.Batch);
                LogService.info("PaymentReceipt processed");
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message + ex.InnerException.StackTrace);
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }         
        }

       
    }
}