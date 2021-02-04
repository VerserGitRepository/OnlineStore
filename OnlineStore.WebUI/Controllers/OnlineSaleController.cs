using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using OnlineStore.WebUI.Infrastructure;
using OnlineStore.WebUI.Infrastructure.HelperServices;
using OnlineStore.WebUI.Models;
using PagedList;

namespace OnlineStore.WebUI.Controllers
{
    public class OnlineSaleController : Controller
    {
        public static List<OnlineSaleProduct> SaleProducts = new List<OnlineSaleProduct>();
        public ActionResult Chat()
        {
            return View();
        }
        public ActionResult SampleCardDesingHome()
        {
           
            return View();
        }
        public ActionResult Index()
        {
            var MainpageproductView = new MainPageProductsViewModel();
            SaleProducts = OrdersServices.OnlineSaleProductsList().Result;
            MainpageproductView.OnlineSaleProductModel = MainPageProductService.MainPageProductsList().Result;
            var prods = SaleProducts.Select(m => m.ProductName).ToArray<string>();
            string prodinfo = "";
            foreach (string s in prods)
            {
                prodinfo += s + ",";
            }

            Session["productinformation"] = prodinfo;
            return View(MainpageproductView);
        }

        public ActionResult SingleProductPage()
        {
            int ProductId = Convert.ToInt32(TempData["productIdcarousel"]);
            var productdeatiledModel = new OnlineSaleProduct();
            productdeatiledModel = OrdersServices.OnlineSaleProductById(ProductId).Result;
            return View(productdeatiledModel);
        }
        [HttpPost]
        public ActionResult SingleProductPage(int ProductId)
        {
            TempData["productIdcarousel"] = ProductId;
            var productdeatiledModel = new OnlineSaleProduct();
            productdeatiledModel = OrdersServices.OnlineSaleProductById(ProductId).Result;

            return Json(new { newUrl = Url.Action("SingleProductPage", "OnlineSale") }, JsonRequestBehavior.AllowGet);
           // return View(productdeatiledModel);         
        }
        public ActionResult Mobile(int Page_No = 1)
        {
            var size = SaleProducts.Where(m => m.ItemType_ID == 15).OrderBy(d => d.QtyAvailable);
            int Size_Of_Page = 10;
            int No_Of_Page = Page_No;
            return View(size.ToPagedList(No_Of_Page, Size_Of_Page));

        }
        public ActionResult CarsoleProductsPage(int Page_No = 1)
        {
            var size = SaleProducts.Where(m => m.IsCarouselProduct ==true);
            int Size_Of_Page = 10;
            int No_Of_Page = Page_No;
            return View(size.ToPagedList(No_Of_Page, Size_Of_Page));
        }
        public ActionResult Laptop(int Page_No = 1)
        {
            var size = SaleProducts.Where(m => m.ItemType_ID == 3).OrderByDescending(d=>d.QtyAvailable);
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
            var size = SaleProducts.Where(m => m.ItemType_ID == 39 || m.ItemType_ID == 40);
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
        public ActionResult Auction()
        {
            return View();
        }
        public ActionResult AllProducts(string product = "")
        {
            if (product != "")
            {
                Session["product"] = product;
                return Json(new { newUrl = Url.Action("ShowProducts", "OnlineSale") }, JsonRequestBehavior.AllowGet);
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
        public ActionResult AddToCart(int id, string returnUrl,  FormCollection  coll, int quantity = 0, decimal price = 0.0M)
        {
            @ShoppingCart p = new @ShoppingCart();
            quantity =  string.IsNullOrEmpty(coll["hdnQty"]) ? quantity : Convert.ToInt32(coll["hdnQty"]); 
            price = string.IsNullOrEmpty(coll["hdnprice"]) ? price : Convert.ToDecimal(coll["hdnprice"]);
            return RedirectToAction("AddToShoppingCart", "ShoppingCart",new {@id=id, @returnUrl  = returnUrl,@quantity= quantity,@price=price });
        }   
        public ActionResult AddToCartNoVerb(int id, string returnUrl,int quantity,decimal ItemPrice)
        {
            @ShoppingCart p = new @ShoppingCart();
            return RedirectToAction("AddToShoppingCart", "ShoppingCart", new { @id = id, @returnUrl = returnUrl, @quantity = quantity, @price = ItemPrice });
        }
        public ActionResult RenderView(string view, string viewName)
        {

            SaleProducts.ToList().ForEach(c => c.IsViewTypeGrid = view == "gridViewBtn");
            return RedirectToAction(viewName);
        }
        [HttpPost]
        public ActionResult PaymentReceipt()
        {
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();

            var CheckoutOrdersPaymentRequestdata = new CheckoutOrdersPaymentModel();
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

                CheckoutOrdersPaymentRequestdata.payment_OrderID = orderNo;
                CheckoutOrdersPaymentRequestdata.paymentAmount = paymentAmount;
                CheckoutOrdersPaymentRequestdata.cardType = cardType;
                CheckoutOrdersPaymentRequestdata.nameOnCard = nameOnCard;
                CheckoutOrdersPaymentRequestdata.CardNumber = truncatedCardNumber;
                CheckoutOrdersPaymentRequestdata.PaymentStatus = paymentStatus.ToString();

                var confirmationRequestReturnFlag = OrdersServices.CheckoutOrdersPaymentRequest(CheckoutOrdersPaymentRequestdata).Result;
                LogService.info("PaymentReceipt processed");
                return RedirectToAction("Index", "OnlineSale");
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message + ex.InnerException.StackTrace);
                return new HttpStatusCodeResult(HttpStatusCode.ExpectationFailed);
            }
        }
        [HttpPost]
        public ActionResult ApplyPromoCode(OrderViewModel model)
        {
            var promocodemodel = new PromoCodeModel { PromoDiscountPercent = model.promoCodeModel.PromoDiscountPercent, PromoEndDate = model.promoCodeModel.PromoEndDate, PromoCode = model.promoCodeModel.PromoCode, PromoStartDate = model.promoCodeModel.PromoStartDate, ProductID_FK = model.promoCodeModel.ProductID_FK };
            var result = OrdersServices.ApplyPromoCode(promocodemodel);
            return RedirectToAction("Index", "OrdersList");
        }
        [HttpGet]
        public ActionResult ApplyPromoCode(string PromoCode)
        {

            var model = new PromoCodeModel();
            model = OrdersServices.ApplyPromoCode(PromoCode).Result;
            if (model.IsValidPromo)
            {
                return Json(model.PromoDiscountPercent, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
           
        }
    }
}