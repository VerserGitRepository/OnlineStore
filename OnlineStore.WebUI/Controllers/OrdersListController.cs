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
            return View();
        }
        [HttpPost]
        public ActionResult ProcessManualOrder(ManualOrdersViewModel manualorderviewmodel)
        {
             var resp=  OrdersServices.ProcessManualOrders(manualorderviewmodel);
             return RedirectToAction("Index", "Orders");
        }
    }
}