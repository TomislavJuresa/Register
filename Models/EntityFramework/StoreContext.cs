using Store.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.EntityFramework
{
    public class StoreContext:DbContext
    {
        public StoreContext() : base()
        {
            this.Database.Connection.ConnectionString = "Server=DESKTOP-Q6NTATU;Database=testttDB;Trusted_Connection=True;";
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<StoreContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Account>().HasKey(e =>new { e.Id, e.Username });
            //modelBuilder.Entity<Account>().has
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Account> Accounts { get; set; }
    public DbSet<Compartment> Compartments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Shelf> Shelves{ get; set; }
    public DbSet<Vendor> Vendors{ get; set; }
        public object Account { get; set; }
    }
}
