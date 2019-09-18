using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Net;
using OnlineStore.WebUI.Models;

namespace OnlineStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private ApplicationData.ApplicationData applicationDataContext;
        private WMSData.WMSData wmsDataContext;

        public NavController()
        {
            string username = WebConfigurationManager.AppSettings["Username"];
            string password = WebConfigurationManager.AppSettings["Password"];

            NetworkCredential credentials = new NetworkCredential(username, password);

            if (applicationDataContext == null)
            {
                Uri applicationDataUri = new Uri(WebConfigurationManager.AppSettings["ApplicationDataUri"]);
                applicationDataContext = new ApplicationData.ApplicationData(applicationDataUri);
                applicationDataContext.Credentials = credentials;
            }

            if (wmsDataContext == null)
            {
                Uri wmsDataUri = new Uri(WebConfigurationManager.AppSettings["WMSDataUri"]);
                wmsDataContext = new WMSData.WMSData(wmsDataUri);
                wmsDataContext.Credentials = credentials;
            }
        }

        public PartialViewResult Menu(string selectedItemType, string customerCode, string saleId)
        {
            var itemTypeIds = applicationDataContext.SaleProducts.Expand("Product, Sale, Sale/Customer").Where(x => x.Sale.Customer.CustomerCode == customerCode && x.Sale.SaleID == saleId).ToList().Select(x => Convert.ToInt32(x.Product.ItemType_ID));
            var itemTypes = wmsDataContext.ItemTypes.ToList().Select(x => new ItemType { Id = Convert.ToInt32(x.ID), Description = x.Description}).Where(x => itemTypeIds.Contains(x.Id));

            MenuViewModel model = new MenuViewModel
            {
                CustomerCode = customerCode,
                SaleId = saleId,
                ItemTypes = itemTypes,
                SelectedItemType = String.IsNullOrEmpty(selectedItemType) ? "All" : selectedItemType
            };

            return PartialView(model);
        }
    }
}