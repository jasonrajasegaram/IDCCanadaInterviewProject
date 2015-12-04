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
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            context.Users.Add(new User() { firstName = "Jason", lastName = "Rajasegaram", userName = "jasonrajasegaram@hotmail.com", isAdmin = true });
        }
    }
}
