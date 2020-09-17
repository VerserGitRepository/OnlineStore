using OnlineStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineStore.WebUI.Infrastructure.HelperServices;
namespace OnlineStore.WebUI.Controllers
{
    public class OrdersListController : Controller
    {
        // GET: OrdersList
        public ActionResult Index()
        {
            var LoadViewModel = new OrderViewModel();

            LoadViewModel.OnlineSaleOrdersList = OrdersServices.OnlineSaleOrdersList().Result;
            LoadViewModel.ManualOrdersViewModel.CustomerProject = new SelectList(DropDownServices.ProjectList().Result, "ID", "Value");
            LoadViewModel.OnlineSaleProduct.ItemTypes = new SelectList(DropDownServices.itemtypes().Result, "ID", "Value");
            LoadViewModel.OnlineSaleProduct.Makes = new SelectList(DropDownServices.Makes().Result, "ID", "Value");
            //LoadViewModel.OnlineSaleProduct.Models = new SelectList(DropDownServices.models().Result, "ID", "Value");

            return View(LoadViewModel);
        }
        [HttpPost]
        public ActionResult ProcessManualOrder(ManualOrdersViewModel manualorderviewmodel)
        {
             var resp=  OrdersServices.ProcessManualOrders(manualorderviewmodel);
             return RedirectToAction("Index", "Orders");
        }

        [HttpPost]
        public ActionResult AddUpdateProduct(OnlineSaleProduct OnlineSaleProductModel)
        {
            var resp = OrdersServices.AddUpdateProduct(OnlineSaleProductModel);
            return RedirectToAction("Index", "Orders");
        }
    }
}