namespace Platform.DataAccess.Resources.Migrations
{
    using Entities;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ResourcesContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ResourcesContext context)
        {
            var data = new List<Contact> {
                new Contact { Name = "John Doe", Address = new Address { AddressLine = "Some street", Zip ="0000" }, PhoneNumber = "+13243296655", ContactType="Employee" },
                new Contact { Name = "John Doe1", Address = new Address { AddressLine = "Some street", Zip ="0000" }, PhoneNumber = "+13254896655", ContactType="Other" },
                new Contact { Name = "John Doe2", Address = new Address { AddressLine = "Some street", Zip ="0000" }, PhoneNumber = "+236745534543", ContactType="Customer" },
                new Contact { Name = "John Doe3", Address = new Address { AddressLine = "Some street", Zip ="0000" }, PhoneNumber = "+3456345345", ContactType="Customer" },
                new Contact { Name = "John Doe4", Address = new Address { AddressLine = "Some street", Zip ="0000" }, PhoneNumber = "+4353453454", ContactType="Employee" },
                new Contact { Name = "John Doe5", Address = new Address { AddressLine = "Some street", Zip ="0000" }, PhoneNumber = "+765756756756", ContactType="Supplier" },
                new Contact { Name = "John Doe6", Address = new Address { AddressLine = "Some street", Zip ="0000" }, PhoneNumber = "+13254896655", ContactType="Customer" },
                new Contact { Name = "John Doe7", Address = new Address { AddressLine = "Some street", Zip ="0000" }, PhoneNumber = "+7567567", ContactType="Customer" },
                new Contact { Name = "John Doe8", Address = new Address { AddressLine = "Some street", Zip ="0000" }, PhoneNumber = "+1324396655", ContactType="Customer" },
                new Contact { Name = "John Doe9", Address = new Address { AddressLine = "Some street", Zip ="0000" }, PhoneNumber = "+345345434", ContactType="Customer" },
                new Contact { Name = "John Doe10", Address = new Address { AddressLine = "Some street", Zip ="0000" }, PhoneNumber = "+13254896655", ContactType="Employee" },
                new Contact { Name = "John Doe11", Address = new Address { AddressLine = "Some street", Zip ="0000" }, PhoneNumber = "+13265426655", ContactType="Supplier" },
            };

            foreach(var item in data)
            {
                context.Contacts.Add(item);
            }

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
        }
    }
}
