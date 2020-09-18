using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.WebUI.Infrastructure.HelperServices;
using OnlineStore.WebUI.Models;
namespace OnlineStore.WebUI.Controllers
{
    public class OnlineSaleController : Controller
    {
      public static  List<OnlineSaleProduct> SaleProducts = new List<OnlineSaleProduct>();
       
        public ActionResult Index()
        {
            SaleProducts = OrdersServices.OnlineSaleProductsList().Result;
            return View();
        }

        public ActionResult Mobile()
        {
          
            return View(SaleProducts.Where(m=>m.ProductName.Contains("Apple")));
        }
        public ActionResult Laptop()
        {
            return View(SaleProducts.Where(m => m.ProductName.Contains("Dell")));          
        }
        public ActionResult Monitor()
        {
            return View(SaleProducts.Where(m => m.ProductName.Contains("Monitor")));
        }
        public ActionResult DesktopPC()
        {
            return View(SaleProducts.Where(m => m.ProductName.Contains("Desktop")));
        }
        public ActionResult IPad()
        {
            return View(SaleProducts.Where(m => m.ProductName.Contains("IPad")));
        }
    }
}