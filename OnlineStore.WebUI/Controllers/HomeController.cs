using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult Upgrade()
        {
            return View();
        }
       
    }
}