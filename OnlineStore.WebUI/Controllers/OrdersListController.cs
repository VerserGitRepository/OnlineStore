using OnlineStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.WebUI.Infrastructure.HelperServices;
using System.Drawing;
using System.IO;

namespace OnlineStore.WebUI.Controllers
{
    public class OrdersListController : Controller
    {
        // GET: OrdersList
        [OutputCache(CacheProfile = "Cache5Min")]
        public ActionResult Index()
        {
            var LoadViewModel = new OrderViewModel();
            LoadViewModel.OnlineSaleOrdersList = OrdersServices.OnlineSaleOrdersList().Result;
            LoadViewModel.OnlineSaleProductList= OrdersServices.OnlineSaleProductsList().Result;
            LoadViewModel.ManualOrdersViewModel.CustomerProject = new SelectList(DropDownServices.ProjectList().Result, "ID", "Value");
            LoadViewModel.OnlineSaleProduct.ItemTypes = new SelectList(DropDownServices.itemtypes().Result, "ID", "Value");
            LoadViewModel.OnlineSaleProduct.Makes = new SelectList(DropDownServices.Makes().Result, "ID", "Value");

            return View(LoadViewModel);
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
                FirstName= _OrderModel.ManualOrdersViewModel.FirstName,
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
               AmazonOrderNo= _OrderModel.ManualOrdersViewModel.AmazonOrderNo,
               ProjectId = _OrderModel.ManualOrdersViewModel.ProjectId,
               RefNo= _OrderModel.ManualOrdersViewModel.RefNo,
               Comments =_OrderModel.ManualOrdersViewModel.Comments

            };
             var resp=  OrdersServices.ProcessManualOrders(manualorderviewmodel);
             return RedirectToAction("Index", "OrdersList");
        }

        [HttpPost]
        public ActionResult AddUpdateProduct(OnlineSaleProduct OnlineSaleProductModel)
        {
            foreach (HttpPostedFileBase file in OnlineSaleProductModel.files)
            {
               file.SaveAs(Path.Combine(Server.MapPath(".") + "//ProductImages//", OnlineSaleProductModel.ProductName+".jpg"));
            }
            var resp = OrdersServices.AddUpdateProduct(OnlineSaleProductModel);
            return RedirectToAction("Index", "OrdersList");
        }
       
        public ActionResult AddProduct()
        {
            var OnlineSaleProduct = new OnlineSaleProduct();
           
            OnlineSaleProduct.ItemTypes = new SelectList(DropDownServices.itemtypes().Result, "ID", "Value");
            OnlineSaleProduct.Makes = new SelectList(DropDownServices.Makes().Result, "ID", "Value");
            //LoadViewModel.OnlineSaleProduct.Models = new SelectList(DropDownServices.models().Result, "ID", "Value");
            return PartialView(OnlineSaleProduct);
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
    }
}