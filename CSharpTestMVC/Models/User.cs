using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace CSharpTestMVC.Models
{
    public class User
    {
        public int ID { get; set; }
        public string userName { get; set;}
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string temporaryPassword { get; set; }
        public bool isAdmin { get; set; }
        public virtual ICollection<Contact> userContacts { get; set; }
    }
    public class UserJSON
    {
        public UserJSON(User aUser)
        {
            string decryptedFirstname = "";
            string decryptedLastname = "";
            AddressBookDB.decrypt(aUser.firstName, ref decryptedFirstname);
            AddressBookDB.decrypt(aUser.lastName, ref decryptedLastname);
            this.ID = aUser.ID;
            this.userName = aUser.userName;
            this.firstName = decryptedFirstname;
            this.lastName = decryptedLastname;
            this.password = aUser.password;
            this.isAdmin = aUser.isAdmin;
            this.userContacts = new List<ContactJSON>();
            if (aUser.userContacts != null && aUser.userContacts.Count > 0)
            {
                foreach (Contact oneC in aUser.userContacts)
                {
                    ContactJSON oneCJSON = new ContactJSON(oneC);
                    this.userContacts.Add(oneCJSON);
                }
            }
        }
        public int ID { get; set; }
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public bool isAdmin { get; set; }
        public List<ContactJSON> userContacts { get; set; }
    }
    public class LoginStatus
    {
        public bool correctPassword { get; set; }
        public bool userFound { get; set; } 
    }
    public class IDCCanadaAddressBook : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}