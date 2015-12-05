﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CSharpTestMVC.Models;

namespace CSharpTestMVC.Controllers
{
    public class AddressBookController : Controller
    {
        // GET: AddressBook
        public ActionResult Index()
        {
            Models.User user = new Models.User();
            user = AddressBookDB.GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}