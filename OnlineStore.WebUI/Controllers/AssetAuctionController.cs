using OnlineStore.WebUI.Infrastructure.HelperServices;
using OnlineStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Controllers
{
    public class AssetAuctionController : Controller
    {
        // GET: AssetAuction
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AuctionAssetBundleBidingDetails(int BundleID)
        {
            var modelDetails = new AssetAuctionBundleModel();
            var modeldata = OrdersServices.AuctionBundleByID(BundleID).Result;
            return View(modeldata);
        }
    }
}