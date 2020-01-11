using JCPCars.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JCPCars.DAL
{
    public class StoreContext : IdentityDbContext<ApplicationUser>
    {
        public StoreContext()
            : base("DefaultConnection")
        {

        }
        static StoreContext()
        {
            Database.SetInitializer<StoreContext>(new StoreInitializer());
        }

        public virtual DbSet<Car> Cars { get; set; }
        public DbSet<Serie> Series { get; set; }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    //        modelBuilder.Entity<ApplicationUser>().HasMany(a => a.Orders).WithRequired().WillCascadeOnDelete(true);
        //    modelBuilder.Entity<Order>().HasRequired(o => o.User);


        //    //.HasForeignKey(p => p.DepartmentId)
        //    //.WillCascadeOnDelete(false);


        //    // Change the name of the table to be Users instead of AspNetUsers
        //    //modelBuilder.Entity<IdentityUser>()
        //    //    .ToTable("Users");
        //    //modelBuilder.Entity<ApplicationUser>()
        //    //    .ToTable("Users");
        //}


        public static StoreContext Create()
        {
            return new StoreContext();
        }



    }
}