using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpTestMVC.Models;

namespace CSharpTestMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            Models.User user = new Models.User();
            user= AddressBookDB.GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (!user.isAdmin)
            {
                return RedirectToAction("Index", "AddressBook");
            }
            return View();
        }
        public JsonResult GetCurrentAdmin()
        {
            User user = new User();
            user = AddressBookDB.GetCurrentUser();
            List<UserJSON> userList = new List<UserJSON>();
            var db = new IDCCanadaAddressBook();
            List<User> users = new List<User>();
            users = db.Users.ToList();
            foreach (User aUser in users)
            {
                UserJSON aUserJson = new UserJSON(aUser);
                userList.Add(aUserJson);
            }
            Admin admin = new Admin();
            admin.userData = new UserJSON(user);
            admin.userList = userList;
            return Json(admin, JsonRequestBehavior.AllowGet);
        }
    }
}