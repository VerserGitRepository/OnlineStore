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
            var MainpageproductView = new MainPageProductsViewModel();            
            SaleProducts = OrdersServices.OnlineSaleProductsList().Result;
            MainpageproductView.OnlineSaleProductModel = MainPageProductService.MainPageProductsList().Result;

            //var OnlinesaleproductModelFeed =  // new List<OnlineSaleProduct>();          
            // OnlinesaleproductModelFeed.Where(p => p.ItemType_ID == 15).OrderByDescending(m=>m.Id).Take(4).ToList();
            //   MainpageproductView.MainPageLaptopProduct = OnlinesaleproductModelFeed.Where(p => p.ItemType_ID == 3).OrderByDescending(m => m.Id).Take(4).ToList();
      

            return View(MainpageproductView);
        }

        public ActionResult Mobile()
        {
          
            return View(SaleProducts.Where(m=>m.ItemType_ID==15));
        }
        public ActionResult Laptop()
        {
            return View(SaleProducts.Where(m => m.ItemType_ID == 3));          
        }
        public ActionResult Monitor()
        {
            return View(SaleProducts.Where(m => m.ItemType_ID == 2));
        }
        public ActionResult DesktopPC()
        {
            return View(SaleProducts.Where(m => m.ItemType_ID == 1));
        }
        public ActionResult IPad()
        {
            return View(SaleProducts.Where(m => m.ItemType_ID == 11));
        }
        public ActionResult Printer()
        {
            return View(SaleProducts.Where(m => m.ItemType_ID == 6));
        }
        public ActionResult Server()
        {
            return View(SaleProducts.Where(m => m.ItemType_ID == 4));
        }
        public ActionResult NetworkGear()
        {
            return View(SaleProducts.Where(m => m.ItemType_ID == 9));
        }
        public ActionResult TV()
        {
            return View(SaleProducts.Where(m => m.ItemType_ID == 46));
        }
        public ActionResult ProductDetail()
        {
            return PartialView();
        }

      [HttpGet]      
        public ActionResult ProductDetail(int ProductId)
        {    

            var productdeatiledModel = new OnlineSaleProduct();
            productdeatiledModel = OrdersServices.OnlineSaleProductById(ProductId).Result;
            return PartialView(productdeatiledModel);
        }
        [HttpPost]
        public ActionResult AddToCart(int id, string returnUrl)
        {
            @ShoppingCart p = new @ShoppingCart();
            return RedirectToAction("AddToShoppingCart", "ShoppingCart",new {@id=id, @returnUrl  = returnUrl });
        }   
        public ActionResult AddToCartNoVerb(int id, string returnUrl)
        {
            @ShoppingCart p = new @ShoppingCart();
            return RedirectToAction("AddToShoppingCart", "ShoppingCart", new { @id = id, @returnUrl = returnUrl });
        }
    }
}