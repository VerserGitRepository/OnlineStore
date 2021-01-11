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
            var bundleslist = OrdersServices.AuctionBundles().Result;          
            return View(bundleslist);
        }
        [HttpPost]
        public ActionResult AuctionAssetBundleBidingDetails(int BundleID)
        {
            var modelDetails = new AssetAuctionBundleModel();
            var modeldata = OrdersServices.AuctionBundleByID(BundleID).Result;
            return View(modeldata);
        }
        [HttpGet]
        public ActionResult _AuctionAssetList()
        {
            var assets = new List<AssetListModel>();
            return PartialView(assets);
        }
        [HttpPost]
        public ActionResult AuctionAssetList(int BundleId)
        {
            var assets = OrdersServices.AuctionAssetList(BundleId).Result;
            return PartialView("_AuctionAssetList",assets);
        }
    }
}