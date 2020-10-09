using Newtonsoft.Json;
using OnlineStore.WebUI.Infrastructure;
using OnlineStore.WebUI.Infrastructure.HelperServices;
using OnlineStore.WebUI.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Controllers
{
    public class ShoppingCartController : Controller
    {      
        public ViewResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Checkout(ShippingDetailsViewModel _checkoutDataModel)
        {
            if (_checkoutDataModel != null)
            {
                var CheckoutOrdersPaymentRequestdata = new CheckoutOrdersPaymentModel();
                _checkoutDataModel.OrderType = "OnlineStore";

                var sc = (ShoppingCart)Session["Productcart"];
                if (sc.Lines.Count() > 0)
                {
                    foreach (var soldproducts in sc.Lines)
                    {
                        _checkoutDataModel.PurchasedSaleProducts.Add(new OnlineSaleProduct
                        {
                            ProductName = soldproducts.SaleProduct.ProductName,
                            Id = soldproducts.SaleProduct.Id,
                            PriceIncGST = soldproducts.SaleProduct.PriceIncGST,
                            PriceExGST = soldproducts.SaleProduct.PriceExGST,
                            GSTAmount = soldproducts.SaleProduct.GSTAmount,
                            PurchasedQty = soldproducts.SaleProduct.PurchasedQty
                        });
                    }
                }
                //var jsondata= JsonConvert.SerializeObject(_checkoutDataModel);
                CheckoutOrdersPaymentRequestdata.CardNumber = _checkoutDataModel.CardNumber;
                CheckoutOrdersPaymentRequestdata.nameOnCard = _checkoutDataModel.nameOnCard;
                CheckoutOrdersPaymentRequestdata.cardType = _checkoutDataModel.cardType;
                CheckoutOrdersPaymentRequestdata.cvv = _checkoutDataModel.cvv;
                CheckoutOrdersPaymentRequestdata.expmonth = _checkoutDataModel.expmonth;
                CheckoutOrdersPaymentRequestdata.expyear = _checkoutDataModel.expyear;
                CheckoutOrdersPaymentRequestdata.paymentAmount = _checkoutDataModel.paymentAmount;
                CheckoutOrdersPaymentRequestdata.payment_OrderID = _checkoutDataModel.payment_OrderID;
                CheckoutOrdersPaymentRequestdata.PaymentStatus = _checkoutDataModel.PaymentStatus;
                CheckoutOrdersPaymentRequestdata.PaymentID = _checkoutDataModel.PaymentID;

                var jsondata2 = JsonConvert.SerializeObject(CheckoutOrdersPaymentRequestdata);

               LogService.info("Order Has been Checked out for payment");

                var returnflag = OrdersServices.OnlineStoreCheckoutOrder(_checkoutDataModel).Result;
                if (returnflag.IsSuccess)
                {
                    CheckoutOrdersPaymentRequestdata.payment_OrderID = returnflag.Id;
                    string url = OrderProcessor.ProcessOnlineSaleOrder(_checkoutDataModel);
                    if (url != null)
                    {                       
                        return Redirect(url);
                        // var confirmationRequestReturnFlag = OrdersServices.CheckoutOrdersPaymentRequest(CheckoutOrdersPaymentRequestdata).Result;
                    }
                }
            }
            return RedirectToAction("Index", "ShoppingCart");
        }
        public ActionResult paymentCheckoutPage(ShippingDetailsViewModel _checkoutDataModel)
        {
            LogService.info("Order Has been Checked out for payment");
            string url = OrderProcessor.ProcessOnlineSaleOrder(_checkoutDataModel);            
            return Redirect(url);
        }

        public PartialViewResult Summary(ShoppingCart cart)
        {
            ShoppingCart item = (ShoppingCart)Session["Productcart"];
            return PartialView(item);
        }  
        public ActionResult Index(string returnUrl)
        {
            ShoppingCart item = (ShoppingCart)Session["Productcart"];
            if (item == null)
            {
                return RedirectToAction("Index", "OnlineSale");
            }
            Session["TotalValue"] = item.ComputeTotalValue();
            return View(new CartIndexViewModel
            {
                ReturnUrl = returnUrl,
                Cart = item
            });            
        }
        public RedirectToRouteResult AddToShoppingCart(int id, string returnUrl)
        {

            var sp = new OnlineSaleProduct();
            sp = OrdersServices.OnlineSaleProductById(id).Result;
            ShoppingCart sc = (ShoppingCart)Session["Productcart"];
            if (sp != null)
            {
                if (sc == null)
                {
                    sc = new ShoppingCart();
                }
                sc.AddItem(sp, 1);
            }
            Session["Productcart"] = sc;
            if (returnUrl == null || returnUrl == string.Empty)
            {
                return RedirectToAction("Index","OnlineSale");

            }
            else
            {
                int Page_No = returnUrl.IndexOf('?') > 0 ? Convert.ToInt32(returnUrl.Split('/')[3].Split('?')[1].Replace("Page_No=","").Trim()) : 1;
                return RedirectToAction(returnUrl.IndexOf('?') > 0 ? returnUrl.Split('/')[3].Split('?')[0]: returnUrl.Split('/')[3], returnUrl.Split('/')[2],new { Page_No});
            }
            
            
        }

        public RedirectToRouteResult RemoveFromShoppingCart( int id,
                string returnUrl)
        {
            ShoppingCart sc = (ShoppingCart)Session["Productcart"];
            ShoppingCartLine saleProduct = sc.Lines.Where(x => x.SaleProduct.Id == id).FirstOrDefault();

            if (saleProduct != null)
            {
                sc.RemoveLine(saleProduct.SaleProduct);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult ShoppingCartSummary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult ShoppingCartCheckout(Cart cart)
        {
            ShippingDetailsViewModel model = new ShippingDetailsViewModel();

            if (cart.Lines.Count() != 0)
            {
                var assetAllocation = cart.Lines.First().AssetAllocation;

                if (assetAllocation != null)
                {
                    model.Email = assetAllocation.EmployeeEmail;
                }
            }
            return View(model);
        }

      
    }
}