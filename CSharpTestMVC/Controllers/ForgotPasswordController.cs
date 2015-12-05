using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpTestMVC.Models;

namespace CSharpTestMVC.Controllers
{
    public class ForgotPasswordController : Controller
    {
        // GET: ForgotPassword
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SendTemporary()
        {
            return View();
        }
        public JsonResult TemporaryPasswordCheck(string userName, string temporaryPassword)
        {
            bool tempCorrect = false;
            tempCorrect=AddressBookDB.TemporaryPasswordCheck(userName, temporaryPassword);
            return Json(tempCorrect, JsonRequestBehavior.AllowGet);
        }
        public JsonResult NewPassword(string userName, string newPassword)
        {
            AddressBookDB.ChangePassword(userName, newPassword);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult SendTemporaryPassword(string userName)
        {
            bool found=AddressBookDB.SendTemporaryPassword(userName);
            return Json(found, JsonRequestBehavior.AllowGet);
        }
    }
}