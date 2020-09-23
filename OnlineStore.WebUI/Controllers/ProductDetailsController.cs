using OnlineStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.WebUI.Infrastructure.HelperServices;
namespace OnlineStore.WebUI.Controllers
{
    public class ProductDetailsController : Controller
    {
        // GET: ProductDetails
        public ActionResult Index(int ProductId)
        {
            var productdeatiledModel = new OnlineSaleProduct();
            productdeatiledModel = OrdersServices.OnlineSaleProductById(ProductId).Result;
            return View(productdeatiledModel);
        }
    }
}