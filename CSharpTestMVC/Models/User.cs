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
        public bool isAdmin { get; set; }
    }
    public class IDCCanadaAddressBook : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}