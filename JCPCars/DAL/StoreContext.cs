using JCPCars.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace JCPCars.DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext()
            : base("DefaultConnection")
        {

        }
        static StoreContext()
        {
            Database.SetInitializer<StoreContext>(new StoreInitializer());
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Serie> Series { get; set; }
    }
}