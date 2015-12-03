using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSharpTestMVC.Models
{
    public class Contact
    {
        public int ID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int phoneNumber { get; set; }
        public string streetName { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
        public string notes { get; set; }

        public User contact_connection { get; set; }
    }
}