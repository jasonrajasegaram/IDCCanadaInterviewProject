using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpTestMVC.Models;

namespace CSharpTestMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public JsonResult GetCurrentUser()
        {
            Models.User currUser = new Models.User();
            currUser = AddressBookDB.GetCurrentUser();
            Models.UserJSON currUserJSON = new Models.UserJSON(currUser);
            return Json(currUserJSON, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateUserInformation(int userID)
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreateNewUser(string firstName, string lastName, string userName, bool isAdmin)
        {
            AddressBookDB.CreateNewUser(userName, firstName, lastName, isAdmin);
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}