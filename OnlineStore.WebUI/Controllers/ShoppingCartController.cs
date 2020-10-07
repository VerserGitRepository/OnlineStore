using OnlineStore.WebUI.Infrastructure;
using OnlineStore.WebUI.Infrastructure.HelperServices;
using OnlineStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public ViewResult Checkout(ShippingDetailsViewModel _checkoutDataModel)
        {
            paymentCheckoutPage();
            return View();
        }
        public ActionResult paymentCheckoutPage()
        {

            LogService.info("Order Has been Checked out for payment");
            string url = OrderProcessor.ProcessOnlineSaleOrder();
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
                int Page_No = returnUrl.IndexOf('?') > 0 ? Convert.ToInt32(returnUrl.Split('/')[2].Split('?')[1].Replace("Page_No=","").Trim()) : 1;
                return RedirectToAction(returnUrl.IndexOf('?') > 0 ? returnUrl.Split('/')[2].Split('?')[0]: returnUrl.Split('/')[2], returnUrl.Split('/')[1],new { Page_No});
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

        //[HttpPost]
        //public ActionResult ShoppingCartCheckout(Cart cart, ShippingDetailsViewModel shippingDetails)
        //{
        //    //if (cart.Lines.Count() == 0)
        //    //{
        //    //    ModelState.AddModelError("", "Sorry, your cart is empty!");
        //    //}

        //    if (ModelState.IsValid)
        //    {
        //        OrderProcessor processor = new OrderProcessor(applicationDataContext, cart, shippingDetails);
        //        LogService.info("Order Has been Checked out for payment");
        //        return Redirect(processor.ProcessOrder());
        //    }
        //    else
        //    {
        //        return View(shippingDetails);
        //    }
        //}
    }
}