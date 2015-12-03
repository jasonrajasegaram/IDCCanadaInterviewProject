using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpTestMVC.Models;

namespace CSharpTestMVC.Controllers
{
    public class ContactController : Controller
    {
        public JsonResult EditContact(int userID, int contactID)
        {
            return Json("");
        }
        public JsonResult DeleteContact(int userID, int contactID)
        {
            return Json("");
        }
        public JsonResult AddNewContact(int userID)
        {
            return Json("");
        }
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
    }
}