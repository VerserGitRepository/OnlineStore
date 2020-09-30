using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Net;
using OnlineStore.WebUI.Models;
using OnlineStore.WebUI.ApplicationData;
using OnlineStore.WebUI.Infrastructure;
using OnlineStore.WebUI.Infrastructure.HelperServices;

namespace OnlineStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private ApplicationData.ApplicationData applicationDataContext;
        private WMSData.WMSData wmsDataContext;

        public CartController()
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

        //public ViewResult Index(Cart cart, string returnUrl)
        //{
        //    return View(new CartIndexViewModel
        //    {
        //        ReturnUrl = returnUrl,
        //        Cart = cart
        //    });
        //}

        public RedirectToRouteResult AddToCart(Cart cart, int id, int? assetAllocationId, string returnUrl)
        {
            var sp = new OnlineSaleProduct();
            sp = OrdersServices.OnlineSaleProductById(id).Result;

            if (sp != null)
            {
                cart.AddItem(sp, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int id,
                string returnUrl)
        {
            SaleProduct saleProduct = applicationDataContext.SaleProducts.Where(x => x.Id == id).FirstOrDefault();

            if (saleProduct != null)
            {
                cart.RemoveLine(saleProduct);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout(Cart cart)
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

        [HttpPost]
        public ActionResult Checkout(Cart cart, ShippingDetailsViewModel shippingDetails)
        {
            //if (cart.Lines.Count() == 0)
            //{
            //    ModelState.AddModelError("", "Sorry, your cart is empty!");
            //}

            if (ModelState.IsValid)
            {
                OrderProcessor processor = new OrderProcessor(applicationDataContext, cart, shippingDetails);
                LogService.info("Order Has been Checked out for payment");
                return Redirect(processor.ProcessOrder());
            }
            else
            {
                return View(shippingDetails);
            }
        }
    }
}