using Microsoft.AspNet.Identity.EntityFramework;
using JCPCars.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using JCPCars.DAL;
using JCPCars.Migrations;
using System.Data.Entity.Migrations;

namespace JCPCars.DAL
{
    public class StoreInitializer : MigrateDatabaseToLatestVersion<StoreContext, Configuration>
    {
        //protected override void Seed(StoreContext context)
        //{
        //    SeedStoreData(context);
        //    base.Seed(context);
        //}

        public static void SeedStoreData(StoreContext context) //metoda wypełniająca db przykładowymi danymi
        {
            var series = new List<Serie>
            {
                new Serie() {SerieId = 1, Name = "Hot-hatch"},
                new Serie() { SerieId = 2, Name = "Limuzyny"},
                new Serie() { SerieId = 3, Name = "Kombi"},
                new Serie() { SerieId = 4, Name = "Cabrio"},
                new Serie() { SerieId = 5, Name = "Supercars"}
            };
            series.ForEach(g => context.Series.AddOrUpdate(g));
            context.SaveChanges();


            var cars = new List<Car>
            {
                new Car() { CarId = 1, CarModel = "Golf GTI", CarBrand = "Volkswagen", Description = "Opis", Price = 100, PictureFileName = "1.png", DateAdded = new DateTime(2019, 12, 1), SerieId = 1 },
                new Car() { CarId = 2, CarModel = "RS3", CarBrand = "Audi", Description = "Opis", Price = 100, PictureFileName = "2.png", DateAdded = new DateTime(2019, 12, 1), SerieId = 1 },
                new Car() { CarId = 3, CarModel = "911", CarBrand = "Porsche", Description = "Opis", Price = 100, PictureFileName = "3.png", DateAdded = new DateTime(2019, 12, 1), SerieId = 4 },
                new Car() { CarId = 4, CarModel = "RS6", CarBrand = "Audi", Description = "Opis", Price = 100, PictureFileName = "4.png", DateAdded = new DateTime(2019, 12, 1), SerieId = 3 },
                new Car() { CarId = 5, CarModel = "Aventador", CarBrand = "Lamborghini", Description = "Opis", Price = 100, PictureFileName = "5.png", DateAdded = new DateTime(2019, 12, 1), SerieId = 5 },
                new Car() { CarId = 6, CarModel = "M5", CarBrand = "BMW", Description = "Opis", Price = 100, PictureFileName = "6.png", DateAdded = new DateTime(2019, 12, 1), SerieId = 2 },
            };

            cars.ForEach(g => context.Cars.AddOrUpdate(g));
            context.SaveChanges();
        }


        public static void InitializeIdentityForEF(StoreContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string name = "admin@spodigly.pl";
            const string password = "P@ssw0rd";
            const string roleName = "Admin";


            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, UserData = new UserData() };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            //var user = userManager.FindByName(name);
            //if (user == null)
            //{
            //    user = new ApplicationUser { UserName = name, Email = name };
            //    var result = userManager.Create(user, password);
            //    result = userManager.SetLockoutEnabled(user.Id, false);
            //}

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }

    }
}