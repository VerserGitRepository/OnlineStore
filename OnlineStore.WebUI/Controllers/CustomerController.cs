using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Net;
using OnlineStore.WebUI.Models;
using System.Configuration;
using OnlineStore.WebUI.Infrastructure;

namespace OnlineStore.WebUI.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationData.ApplicationData applicationDataContext;
        private WMSData.WMSData wmsDataContext;

        public CustomerController()
        {
            string username = WebConfigurationManager.AppSettings["Username"];
            string password = WebConfigurationManager.AppSettings["Password"];

            NetworkCredential credentials = new NetworkCredential(username, password);

            if(applicationDataContext == null)
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

        public ActionResult Welcome(string customerCode, string saleId)
        {
            var customer = applicationDataContext.Customers.Where(x => x.CustomerCode == customerCode).SingleOrDefault();
            var sale = applicationDataContext.Sales.Where(x => x.Customer.CustomerCode == customerCode).SingleOrDefault();
            var model = new WelcomeViewModel { Customer = customer, Sale = sale };
            if (customer != null)
            {
                Session["CustomerName"] = customer.CustomerName;
                Session["CustomerCode"] = customer.CustomerCode;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Welcome(WelcomeViewModel model,string Salecode)
        {
            var customer = applicationDataContext.Customers.Where(x => x.Id == model.Customer.Id).Single();
            var sale = applicationDataContext.Sales.Where(x => x.Id == model.Sale.Id).Single();

            if (sale.SaleType == "Pre-Allocated")
            {
                //if (string.IsNullOrEmpty(model.EmployeeEmail))
                //{
                //    ModelState.AddModelError("EmployeeEmail", "Please enter an email address");
                //}

                if (string.IsNullOrEmpty(model.EmployeeID))
                {
                    ModelState.AddModelError("EmployeeID", "Please enter your employee ID");
                }

                if (ModelState.IsValid)
                {
                    //   var assetAllocations = applicationDataContext.AssetAllocations.Where(x => x.EmployeeEmail == model.EmployeeEmail && x.EmployeeID == model.EmployeeID);
                    var assetAllocations = applicationDataContext.AssetAllocations.Where(x => x.EmployeeID == model.EmployeeID);

                    if (assetAllocations.Count() == 0)
                    {
                        ModelState.AddModelError(string.Empty, "Employee not found");
                    }
                }

                Session["employeeId"] = model.EmployeeID;
            }

           // string salesCode = Salecode.ToString();
           //if (string.IsNullOrEmpty(salesCode))
           // {
           //     ModelState.AddModelError("Sale Code", "Please enter Sale Code");
           //     Session["ErrorMessage"] = "Please enter Sale Code";

           // }
           //if(!string.IsNullOrEmpty(salesCode) && salesCode != sale.SaleID)
           // {
           //     ModelState.AddModelError("Sale Code", "Sale Code Not Matched!");

           //     Session["ErrorMessage"]="Sale Code Not Matched!";

           // }
            if (ModelState.IsValid)
            {
                return RedirectToAction("List", "Sale", new { customerCode = customer.CustomerCode, saleId = sale.SaleID });
             }

            model.Customer = customer;
            model.Sale = sale;

            return View(model);
        }

        public ActionResult GetCustomerLogo(int customerId)
        {
            string encoding = "image/jpg";
            WebImage image = new WebImage("~/Content/Images/verserlogo.png");

            var customer = applicationDataContext.Customers.Where(x => x.Id == customerId).SingleOrDefault();

            if(customer != null)
            {
                var logo = customer.Logo;

                if(logo != null && logo.Length > 0)
                {
                    image = new WebImage(logo);

                    switch (image.ImageFormat)
                    {
                        case "jpeg":
                            encoding = "image/jpg";
                            break;
                        case "png":
                            encoding = "image/png";
                            break;
                    }
                }
            }

            return File(image.GetBytes(), encoding);
        }
    }
}