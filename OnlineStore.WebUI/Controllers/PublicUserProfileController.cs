using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Controllers
{
    public class PublicUserProfileController : Controller
    {
        // GET: PublicUserProfile
        public ActionResult Index()
        {
            return View();
        }
    }
}