using Models.EntityFramework;
using Store.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRun
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("it runs");
            var employee = new Employee
            {
                Name = "Tomislav",
                Surname = "Juresa",
                Adress = "street 2",
                City = "Karlsruhe",
                PostalCode = 76180,
                Country = "Germany",
                Notes = "App developer"
            };
            employee.ReportsToEmployeeID = employee.Id;
            var account = new Account
            {
                CreatedOn = DateTime.Now,
                DisplayUsername = "juresa",
                Employee = employee,
                Password = "admin",
                Role = Role.SuperAdmin,
                Username = "tjuresa"

            };
            account.CreatedByAccountID = account.Id;
            var vendor = new Vendor
            {
                CompanyName = "Factory",
                Adress = "adress",
                City = "Karlsruhe",
                PostalCode = 123,
                Country = "Germany",
                Phone = "12345",
                CreatedOn=DateTime.Now
            };
            var product = new Product
            {
                Name = "Monitor",
                Price = 22.12,
                Vendor = vendor,
                CreatedOn=DateTime.Now

            };
            vendor.Products.Add(product);
            using (var context = new StoreContext() {})
            {


                context.Accounts.Add(account);
                context.Vendors.Add(vendor);
                context.SaveChanges();
            }
            Console.ReadKey();
        }
    }
}
