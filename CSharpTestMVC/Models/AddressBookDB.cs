using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSharpTestMVC.Models
{
    public class AddressBookDB
    {
        public static UInt32 createNewUser(string userName, string firstName, string lastName)
        {
            UInt32 retVal = 0;
            User newUser = new User();
            newUser.userName = userName;
            newUser.firstName = firstName;
            newUser.lastName = lastName;
            var db = new IDCCanadaAddressBook();
            db.Users.Add(newUser);
            db.SaveChanges();
            return retVal;
        }
    }
}