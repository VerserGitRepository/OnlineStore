using OnlineStore.WebUI.Infrastructure.HelperServices;
using OnlineStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

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

            //(2021, 12, 25)
           var modeldata = OrdersServices.AuctionBundleByID(BundleID).Result;
            modeldata.AuctionEndDateStringFormat = $"{modeldata.Bundle_Auction_EndDate.Year}, {modeldata.Bundle_Auction_EndDate.Month}, {modeldata.Bundle_Auction_EndDate.Day}";
            return View(modeldata);
        }
        [HttpGet]
        public ActionResult _AuctionAssetList()
        {
            var assets = new List<AssetListModel>();
            return PartialView(assets);
        }
        [HttpGet]
        public ActionResult AuctionAssetList(int BundleId)
        {
            var assets = OrdersServices.AuctionAssetList(BundleId).Result;
            return PartialView("_AuctionAssetList",assets);
        }
        [HttpPost]
        public ActionResult BuyAuctionBundle(int BundleId)
        {
            if (Session["Username"] !=null)
            {
                var purchaseRequest = new ListItems { 
                    ID= BundleId,Value= Session["Username"].ToString()
                };                   
                var PurchaseResponse = OrdersServices.BuyAuctionBundle(purchaseRequest).Result;
                return RedirectToAction("Index", "AssetAuction");
            }           
                return RedirectToAction("Login", "Login");     
        }

        [HttpPost]
        public ActionResult ExportAuctionResult(int BundleId)
        {
            var assets = OrdersServices.AuctionAssetList(BundleId).Result;
            GridView gv = new GridView();
            gv.DataSource = assets;

            gv.DataBind();
            foreach (GridViewRow row in gv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (row.RowType == DataControlRowType.DataRow)
                    {
                        row.Cells[i].Attributes.Add("class", "text");
                    }
                }
            }
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=AssetsList.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index", "AssetAuction");
        }

        [HttpPost]
        public ActionResult Create()
        {
            var fileType = Request.Form["FileUpload"];

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                                                            //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
             
                string mimeType = file.ContentType;
                if (!Directory.Exists(Server.MapPath("..") + "//ProductImages//"))
                {
                    Directory.CreateDirectory(Server.MapPath("..") + "//ProductImages//");
                }
                file.SaveAs(Server.MapPath("..") + "//ProductImages//" + Request.Files[i].FileName);
            }

            return Json(null);
        }
        [HttpPost]
        public ActionResult Update()
        {
            var fileType = Request.Form["FileUpload"];

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i]; //Uploaded file
                                                            //Use the following properties to get file's name, size and MIMEType
                int fileSize = file.ContentLength;
              
                string mimeType = file.ContentType;
                if (!Directory.Exists(Server.MapPath("..") + "//ProductImages//"))
                {
                    Directory.CreateDirectory(Server.MapPath("..") + "//ProductImages//");
                }
                file.SaveAs(Server.MapPath("..") + "//ProductImages//" + Request.Files[i].FileName);
            }

            return Json(null);
        }

        [HttpGet]
        public ActionResult _AuctionCounterClock()
        {
            return PartialView();
        }

        public string Calculatecountdowntimer(DateTime AuctionEndDate )
        {
           // DateTime daysLeft = DateTime.Parse(AuctionEndDate);
            DateTime startDate = DateTime.Now;          
            TimeSpan t = AuctionEndDate - startDate;
            string countDown = string.Format("{0} Days, {1} Hours, {2} Minutes, {3} Seconds til launch.", t.Days, t.Hours, t.Minutes, t.Seconds);
            return countDown;            
        }
    }
}