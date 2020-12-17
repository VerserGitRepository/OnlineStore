using OnlineStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.WebUI.Infrastructure.HelperServices;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;

namespace OnlineStore.WebUI.Controllers
{
    public class OrdersListController : Controller
    {
        // GET: OrdersList
        //[OutputCache(CacheProfile = "Cache5Min")]
        public ActionResult Index()
        {
            if (Session["Username"] != null)
            {
                var LoadViewModel = new OrderViewModel();
                LoadViewModel.OnlineSaleOrdersList = OrdersServices.OnlineSaleOrdersList().Result;
                foreach (OnlineSaleOrdersListModel s in LoadViewModel.OnlineSaleOrdersList)
                {
                    foreach (OnlineSaleProduct t in s.OnlineSalePurchasedProducts)
                    {
                        s.OnlineSalePurchasedProductsDesc += t.ProductName + "-" + t.ModelName + Environment.NewLine;
                    }
                }
                LoadViewModel.OnlineSaleProductList = OrdersServices.OnlineSaleProductsList().Result;
                //LoadViewModel.OnlineSalePurchasedProducts = OrdersServices.OnlineSaleShippedOrder().Result;

                LoadViewModel.ManualOrdersViewModel.CustomerProject = new SelectList(DropDownServices.ProjectList().Result, "ID", "Value");
                LoadViewModel.OnlineSaleProduct.ItemTypes = new SelectList(DropDownServices.itemtypes().Result, "ID", "Value");
                LoadViewModel.OnlineSaleProduct.Makes = new SelectList(DropDownServices.Makes().Result, "ID", "Value");
                Session["OrderViewModel"] = LoadViewModel;
                return View(LoadViewModel);
            }

            return RedirectToAction("Login","Login");
           
        }
        [HttpPost]
        public ActionResult ProcessManualOrder(OrderViewModel _OrderModel)
        {
            if (_OrderModel.ManualOrdersViewModel==null)
            {
                return RedirectToAction("Index", "OrdersList");
            }
            var manualorderviewmodel = new ManualOrdersViewModel
            {
                FirstName = _OrderModel.ManualOrdersViewModel.FirstName,
                LastName = _OrderModel.ManualOrdersViewModel.LastName,
                Salutation = _OrderModel.ManualOrdersViewModel.Salutation,
                AddressLine1 = _OrderModel.ManualOrdersViewModel.AddressLine1,
                AddressLine2 = _OrderModel.ManualOrdersViewModel.AddressLine2,
                Locality = _OrderModel.ManualOrdersViewModel.Locality,
                Email = _OrderModel.ManualOrdersViewModel.Email,
                State = _OrderModel.ManualOrdersViewModel.State,
                Postcode = _OrderModel.ManualOrdersViewModel.Postcode,
                ContactNumber = _OrderModel.ManualOrdersViewModel.ContactNumber,
                Height = _OrderModel.ManualOrdersViewModel.Height,
                Weight = _OrderModel.ManualOrdersViewModel.Weight,
                Width = _OrderModel.ManualOrdersViewModel.Width,
                Length = _OrderModel.ManualOrdersViewModel.Length,
                NoOfItems = _OrderModel.ManualOrdersViewModel.NoOfItems,
                SSN = _OrderModel.ManualOrdersViewModel.SSN,
                IsExport = _OrderModel.ManualOrdersViewModel.IsExport,
                OrderDate = DateTime.Now,
                AmazonOrderNo = _OrderModel.ManualOrdersViewModel.AmazonOrderNo,
                ProjectId = _OrderModel.ManualOrdersViewModel.ProjectId,
                RefNo = _OrderModel.ManualOrdersViewModel.RefNo,
                OrderType = "ThirdPartyOrder",
               Comments =_OrderModel.ManualOrdersViewModel.Comments
               

            };
             var resp=  OrdersServices.ProcessManualOrders(manualorderviewmodel);
             return RedirectToAction("Index", "OrdersList");
        }

        [HttpPost]
        public ActionResult AddUpdateProduct(OnlineSaleProduct OnlineSaleProductModel)
        {
            if(!ModelState.IsValid)
                return RedirectToAction("Index", "OrdersList");
            string filePath = Path.Combine(Server.MapPath(".."),@"ProductImages\");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            foreach (HttpPostedFileBase file in OnlineSaleProductModel.files)
            {
                if (file != null)
                {
                    file.SaveAs(filePath + file.FileName + ".jpg");
                    OnlineSaleProductModel.Images.Add(file.FileName + ".jpg");
                }
            }
            var jsondata = JsonConvert.SerializeObject(OnlineSaleProductModel);
            var resp = OrdersServices.AddUpdateProduct(OnlineSaleProductModel);
            return RedirectToAction("Index", "OrdersList");
        }
       
        public ActionResult AddProduct()
        {
            var OnlineSaleProduct = new OnlineSaleProduct();
            OnlineSaleProduct.IsUpdateProduct = false;
            OnlineSaleProduct.ItemTypes = new SelectList(DropDownServices.itemtypes().Result, "ID", "Value");
            OnlineSaleProduct.Makes = new SelectList(DropDownServices.Makes().Result, "ID", "Value");
            //LoadViewModel.OnlineSaleProduct.Models = new SelectList(DropDownServices.models().Result, "ID", "Value");
            return PartialView(OnlineSaleProduct);
        }

        public ActionResult ProductList(int id)
        {
            var LoadViewModel = new OrderViewModel();
            LoadViewModel.OnlineSaleOrdersList = OrdersServices.OnlineSaleOrdersList().Result;
            LoadViewModel.OnlineSaleOrdersList = LoadViewModel.OnlineSaleOrdersList.Where(i => i.Id == id).ToList();
            return PartialView(LoadViewModel.OnlineSaleOrdersList.First());
        }

        [HttpPost]
        public ActionResult SendShipment(OnlineSaleOrdersListModel model)
        {
            var shipOrderModel = new ShipStoreOrderModel();
            shipOrderModel.Orderno = model.Id;
            shipOrderModel.Products = model.OnlineSalePurchasedProducts.Select(t => new StoreProductsModel { ProductID = t.Id, SSN = model.SSN }).ToList();

            var returnModel = OrdersServices.SendShipment(shipOrderModel);
            return RedirectToAction("Index");
        }
        public ActionResult UpdateProduct(int productId)
        {
            var OnlineSaleProduct =  OrdersServices.OnlineSaleProductById(productId).Result;
            OnlineSaleProduct.IsUpdateProduct = true;
            OnlineSaleProduct.ItemTypes = new SelectList(DropDownServices.itemtypes().Result, "ID", "Value");
            OnlineSaleProduct.Makes = new SelectList(DropDownServices.Makes().Result, "ID", "Value");
            return PartialView("AddProduct",OnlineSaleProduct);
        }
        public void imageimport()
        {
            //LoadViewModel.OnlineSaleProduct.Models = new SelectList(DropDownServices.models().Result, "ID", "Value");
            //foreach (var item in LoadViewModel.OnlineSaleProductList)
            //{
            //    if (item.ProductImage.Length == 0)
            //        continue;
            //    Image rImage = null;
            //    try
            //    {
            //        using (MemoryStream ms = new MemoryStream(item.ProductImage))
            //        {
            //            using (rImage = Image.FromStream(ms))
            //            {
            //                string FileName = string.Join("_", item.ProductName.Trim().Split(Path.GetInvalidFileNameChars()));
            //                rImage.Save(@"C:\VerserSourceCodeGitRepo\VerserOnlineStore\OnlineStore\OnlineStore.WebUI\ProductImages\" + item.Id + "_" + FileName + ".jpg");
            //            }
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        continue;
            //    }               
            //}

        }
        public JsonResult Models(int makeId, int itemTypeId)
        {
            List<ListItems> Models = new List<ListItems>();
            Models = DropDownServices.models(makeId,itemTypeId).Result.Select(x => new ListItems()
            {
                ID = x.ID,
                Value = x.ModelName
            }).ToList();
            return Json(Models, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult AddPromo()
        {
            var LoadViewModel = (OrderViewModel)Session["OrderViewModel"];
            return PartialView("PromoCode", LoadViewModel);
        }

       
        public ActionResult _AddNewBundle()
        {
         //   var LoadViewModel = (OrderViewModel)Session["OrderViewModel"];
            return PartialView("_AddNewBundle");
        }        
    }
}