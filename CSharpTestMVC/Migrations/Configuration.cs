namespace CSharpTestMVC.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using CSharpTestMVC.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<CSharpTestMVC.Models.IDCCanadaAddressBook>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CSharpTestMVC.Models.IDCCanadaAddressBook context)
        {
            
        }
    }
}
