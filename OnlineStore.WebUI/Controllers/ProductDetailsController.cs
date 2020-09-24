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
        public ActionResult Index()
        {
            int ProductId = 0;
            ProductId = 1; //Convert.ToInt32(TempData["ProductId"]);
            var productdeatiledModel = new OnlineSaleProduct();
            productdeatiledModel = OrdersServices.OnlineSaleProductById(ProductId).Result;
            return View(productdeatiledModel);
        }
        //public ActionResult Index()
        //{
        //    var productdeatiledModel = new OnlineSaleProduct();
        //   // productdeatiledModel = OrdersServices.OnlineSaleProductById(ProductId).Result;
        //    return View(productdeatiledModel);
        //}
    }
}