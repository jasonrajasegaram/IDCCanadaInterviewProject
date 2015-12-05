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
            //AddressBookDB.SendEmail();
            Models.User currUser = new Models.User();
            currUser = AddressBookDB.GetCurrentUser();
            if (currUser != null)
            {
                Models.UserJSON currUserJSON = new Models.UserJSON(currUser);
                return Json(currUserJSON, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult UpdateUserInformation(int userID)
        {
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddNewUser(string firstName, string lastName, string userName, bool isAdmin)
        {
            bool userExists = false;
            AddressBookDB.AddNewUser(userName, firstName, lastName, isAdmin, ref userExists);
            return Json(userExists, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteUser(string userNumber)
        {
            int userID = Convert.ToInt32(userNumber);
            AddressBookDB.DeleteUser(userID);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult EditUser(string userID, string firstName, string lastName, string userName, bool? isAdmin)
        {
            int userIDNumber = Convert.ToInt32(userID);
            AddressBookDB.EditUser(userIDNumber, firstName, lastName, userName, isAdmin);
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}