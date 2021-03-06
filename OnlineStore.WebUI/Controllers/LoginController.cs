﻿using OnlineStore.WebUI.Infrastructure.HelperServices;
using OnlineStore.WebUI.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            Session["Username"] = null;
            Session["FullName"] = null;
            Session["ErrorMessage"] = null;
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
            {
                Session.Clear();
                Session.RemoveAll();
                Session.Abandon();
                Session["ErrorMessage"] = "Username Or Password Is Empty!";
                return View("Login");
            }
            LoginModel user = new LoginModel { UserName = login.UserName, Password = login.Password };
            Task<LoginModel> userReturn = LoginService.Login(user);
            if (userReturn.Result.IsLoggedIn == true)
            {
                Session["Username"] = login.UserName;
                Session["FullName"] = userReturn.Result.FullName;
                Session["ErrorMessage"] = null;
                var Roles = LoginService.UserRoleList(login.UserName).Result;
                if (Roles.Where(x=>x.Value.Contains("Administrator") || x.Value.Contains("Accounts") || x.Value.Contains("Verser Admin") || x.Value.Contains("Salesperson")).FirstOrDefault() !=null)
                {
                    Session["SiteAdmin"] = "True";
                    return RedirectToAction("Index", "OrdersList");
                }
                else
                {
                   
                    Session["SiteAdmin"] = "False";
                    return RedirectToAction("Index", "PublicUserProfile");
                }
            }
            else if (LoginService.OnlineStoreLogin(login).Result.IsLoggedIn)
            {
                Session["SiteAdmin"] = "False";
                Session["Username"] = login.UserName;
               // Session["FullName"] = login.FullName;
                return RedirectToAction("Index", "PublicUserProfile");
            }
            else
            {
                Session["ErrorMessage"] = "Invalid UserName Or Password";
                TempData["ErrorMessage"] = "Invalid UserName Or Password";
                Session.Clear();
                Session.RemoveAll();
                return View("Login");
            }
        }
        public ActionResult Logout(LoginModel login)
        {
            Session["Username"] = null;
            Session["FullName"] = null;
            Session["ErrorMessage"] = null;
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
        public ActionResult _RegisterNewUser()
        {
            return PartialView();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _RegisterNewUser(LoginModel NewUserRegisterModel)
        {
            if (ModelState.IsValid)
            {
                var _t = OrdersServices.CreateOnlineStoreUserAccount(NewUserRegisterModel);
                return RedirectToAction("Index", "OnlineSale");
            }
            return View();
        }

    }
}