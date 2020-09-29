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
        public ViewResult Index() {         
            return View();
        }
        public ViewResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Checkout(ShippingDetailsViewModel _checkoutDataModel)
        {
            return View();
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

  
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                ReturnUrl = returnUrl,
                Cart = cart
            });
        }

        public RedirectToRouteResult AddToShoppingCart(OnlineSaleProduct Productcart, int id, string returnUrl)
        {
            var sp = new OnlineSaleProduct();
            sp = OrdersServices.OnlineSaleProductById(id).Result;

            if (sp != null)
            {
              //  Productcart.AddItem(sp, 1, aa);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        //public RedirectToRouteResult RemoveFromShoppingCart(Cart cart, int id,
        //        string returnUrl)
        //{
        //    SaleProduct saleProduct = applicationDataContext.SaleProducts.Where(x => x.Id == id).FirstOrDefault();

        //    if (saleProduct != null)
        //    {
        //        cart.RemoveLine(saleProduct);
        //    }

        //    return RedirectToAction("Index", new { returnUrl });
        //}

        //public PartialViewResult ShoppingCartSummary(Cart cart)
        //{
        //    return PartialView(cart);
        //}

        //public ViewResult ShoppingCartCheckout(Cart cart)
        //{
        //    ShippingDetailsViewModel model = new ShippingDetailsViewModel();

        //    if (cart.Lines.Count() != 0)
        //    {
        //        var assetAllocation = cart.Lines.First().AssetAllocation;

        //        if (assetAllocation != null)
        //        {
        //            model.Email = assetAllocation.EmployeeEmail;
        //        }
        //    }

        //    return View(model);
        //}

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