using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.WebUI.Infrastructure.HelperServices;
using OnlineStore.WebUI.Models;
using PagedList;

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

        public ActionResult Mobile( int Page_No=1)
        {           
            var size = SaleProducts.Where(m => m.ItemType_ID == 15);
            int Size_Of_Page = 10;
            int No_Of_Page = Page_No;
            return View(size.ToPagedList(No_Of_Page, Size_Of_Page));
            
        }
        public ActionResult Laptop(int Page_No = 1)
        {
            var size = SaleProducts.Where(m => m.ItemType_ID == 3);
            int Size_Of_Page = 10;
            int No_Of_Page = Page_No;
            return View(size.ToPagedList(No_Of_Page, Size_Of_Page));
        }
        public ActionResult Monitor(int Page_No = 1)
        {
            var size = SaleProducts.Where(m => m.ItemType_ID == 2);
            int Size_Of_Page = 10;
            int No_Of_Page = Page_No;
            return View(size.ToPagedList(No_Of_Page, Size_Of_Page));
        }
        public ActionResult DesktopPC(int Page_No = 1)
        {
            var size = SaleProducts.Where(m => m.ItemType_ID == 1);
            int Size_Of_Page = 10;
            int No_Of_Page = Page_No;
            return View(size.ToPagedList(No_Of_Page, Size_Of_Page));
        }
        public ActionResult IPad(int Page_No = 1)
        {
            var size = SaleProducts.Where(m => m.ItemType_ID == 11);
            int Size_Of_Page = 10;
            int No_Of_Page = Page_No;
            return View(size.ToPagedList(No_Of_Page, Size_Of_Page));
        }
        public ActionResult Printer(int Page_No = 1)
        {
            var size = SaleProducts.Where(m => m.ItemType_ID == 6);
            int Size_Of_Page = 10;
            int No_Of_Page = Page_No;
            return View(size.ToPagedList(No_Of_Page, Size_Of_Page));
        }
        public new ActionResult Server(int Page_No = 1)
        {
            var size = SaleProducts.Where(m => m.ItemType_ID == 4);
            int Size_Of_Page = 10;
            int No_Of_Page = Page_No;
            return View(size.ToPagedList(No_Of_Page, Size_Of_Page));
        }
        public ActionResult NetworkGear(int Page_No = 1)
        {
            var size = SaleProducts.Where(m => m.ItemType_ID == 9);
            int Size_Of_Page = 10;
            int No_Of_Page = Page_No;
            return View(size.ToPagedList(No_Of_Page, Size_Of_Page));
        }
        public ActionResult TV(int Page_No = 1)
        {
            var size = SaleProducts.Where(m => m.ItemType_ID == 46);
            int Size_Of_Page = 10;
            int No_Of_Page = Page_No;
            return View(size.ToPagedList(No_Of_Page, Size_Of_Page));
        }
     
        public ActionResult AllProducts(string product="")
        {
            if (product != "")
            {
                Session["product"] = product;                              
                return Json(new { newUrl = Url.Action("ShowProducts", "OnlineSale") },JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("Index");

        }
        public ActionResult ShowProducts(int Page_No = 1)
        {
            string product = (string)Session["product"];
            var size = SaleProducts.Where(m => m.ProductName.ToLower().Contains(product.ToLower()));
            int Size_Of_Page = 10;
            int No_Of_Page = Page_No;
            return View(size.ToPagedList(No_Of_Page, Size_Of_Page));
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