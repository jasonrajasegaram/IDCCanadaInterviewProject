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
            return Json("");
        }
        public JsonResult UpdateUserInformation(int userID)
        {
            return Json("");
        }
    }
}