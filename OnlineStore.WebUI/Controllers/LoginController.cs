﻿using OnlineStore.WebUI.Infrastructure.HelperServices;
using OnlineStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OnlineStore.WebUI.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
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
                return RedirectToAction("Index", "OrdersList");
            }
            else
            {
                Session["ErrorMessage"] = "Invalid UserName Or Password";
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
    }
}