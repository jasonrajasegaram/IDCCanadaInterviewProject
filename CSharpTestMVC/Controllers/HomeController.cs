using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpTestMVC.Models;

namespace CSharpTestMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public JsonResult Login(string username, string password)
        {
            LoginStatus loginStatus = new LoginStatus();
            Models.User user = new Models.User();
            AddressBookDB.GetUserByUsername(username, ref user);
            if (user != null)
            {
                loginStatus.userFound = true;
                string userPassword="";
                AddressBookDB.decrypt(user.password, ref userPassword);
                if (userPassword == password)
                {
                    loginStatus.correctPassword = true;
                    AddressBookDB.SetSessionUser(user);
                }
                else
                {
                    loginStatus.correctPassword = false;
                }
            }
            else
            {
                loginStatus.correctPassword = false;
                loginStatus.userFound = false;
            }

            return Json(loginStatus, JsonRequestBehavior.AllowGet);
        }
    }
}