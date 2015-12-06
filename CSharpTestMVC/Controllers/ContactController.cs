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
        [ValidateInput(false)]
        public JsonResult EditContact(string contactID, string firstName, string lastName, string phoneNumber, string streetName, string city, string province, string postalCode, string country,string notes)
        {
            int contactIDNumber = Convert.ToInt32(contactID);
            AddressBookDB.EditContact(contactIDNumber, firstName, lastName, phoneNumber, streetName, city, province, postalCode, country, notes);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteContact(string contactNumber)
        {
            int contactID = Convert.ToInt32(contactNumber);
            AddressBookDB.DeleteContact(contactID);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        [ValidateInput(false)]
        public JsonResult AddNewContact(string firstName, string lastName, string phoneNumber, string streetName, string city, string province, string postalCode, string country, string notes)
        {
            AddressBookDB.CreateNewContact(firstName, lastName, phoneNumber, streetName, city, province, postalCode, country, notes);
            return Json("", JsonRequestBehavior.AllowGet);
        }
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }
    }
}