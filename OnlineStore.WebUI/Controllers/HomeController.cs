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
            //    return Redirect("https://www.payway.com.au/OnlinePaymentServlet2?ActionContextId=FjgYvYtDhVV8AejclwJmMw&communityCode=PAYWAY&page=enterAccountDetails"); //124.170.239.130 home IP
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