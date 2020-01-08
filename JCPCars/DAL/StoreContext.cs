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
         

        public static StoreContext Create()
        {
            return new StoreContext();
        }


        public DbSet<Car> Cars { get; set; }
        public DbSet<Serie> Series { get; set; }

    }
}