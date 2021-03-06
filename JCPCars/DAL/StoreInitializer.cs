﻿using Microsoft.AspNet.Identity.EntityFramework;
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
        //    InitializeIdentityForEF(context);
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
                new Car() { CarId = 1, CarModel = "Golf R", CarBrand = "Volkswagen", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris dui felis, egestas sit amet sem eget, ullamcorper pulvinar velit. In vel facilisis nisi, pharetra dictum dui.", PricePer1h = 500, PricePer24h = 1000, PricePer1m = 51000, PictureFileName = "1.png", DateAdded = new DateTime(2019, 12, 1), SerieId = 1 },
                new Car() { CarId = 2, CarModel = "RS3", CarBrand = "Audi", Description = "Opis", PricePer1h = 500, PricePer24h = 1200, PricePer1m = 5000, PictureFileName = "2.png", DateAdded = new DateTime(2019, 12, 1), SerieId = 1 },
                new Car() { CarId = 3, CarModel = "911", CarBrand = "Porsche", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris dui felis, egestas sit amet sem eget, ullamcorper pulvinar velit. In vel facilisis nisi, pharetra dictum dui.", PricePer1h = 500, PricePer24h = 1000, PricePer1m = 5000, PictureFileName = "3.png", DateAdded = new DateTime(2019, 12, 1), SerieId = 4 },
                new Car() { CarId = 4, CarModel = "RS6", CarBrand = "Audi", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris dui felis, egestas sit amet sem eget, ullamcorper pulvinar velit. In vel facilisis nisi, pharetra dictum dui.", PricePer1h = 500, PricePer24h = 1000, PricePer1m = 50100, PictureFileName = "4.png", DateAdded = new DateTime(2019, 12, 1), SerieId = 3 },
                new Car() { CarId = 5, CarModel = "Aventador", CarBrand = "Lamborghini", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris dui felis, egestas sit amet sem eget, ullamcorper pulvinar velit. In vel facilisis nisi, pharetra dictum dui.", PricePer1h = 500, PricePer24h = 1000, PricePer1m = 5000, PictureFileName = "5.png", DateAdded = new DateTime(2019, 12, 1), SerieId = 5 },
                new Car() { CarId = 6, CarModel = "M5", CarBrand = "BMW", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris dui felis, egestas sit amet sem eget, ullamcorper pulvinar velit. In vel facilisis nisi, pharetra dictum dui.", PricePer1h = 500, PricePer24h = 1200, PricePer1m = 50500, PictureFileName = "6.png", DateAdded = new DateTime(2019, 12, 1), SerieId = 2 },
                new Car() { CarId = 6, CarModel = "M6", CarBrand = "BMW", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris dui felis, egestas sit amet sem eget, ullamcorper pulvinar velit. In vel facilisis nisi, pharetra dictum dui.", PricePer1h = 500, PricePer24h = 1200, PricePer1m = 50500, PictureFileName = "6.png", DateAdded = new DateTime(2019, 12, 1), SerieId = 2 },
                new Car() { CarId = 6, CarModel = "M4", CarBrand = "BMW", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris dui felis, egestas sit amet sem eget, ullamcorper pulvinar velit. In vel facilisis nisi, pharetra dictum dui.", PricePer1h = 500, PricePer24h = 1300, PricePer1m = 50500, PictureFileName = "6.png", DateAdded = new DateTime(2019, 12, 1), SerieId = 2 },
                new Car() { CarId = 6, CarModel = "M8", CarBrand = "BMW", Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris dui felis, egestas sit amet sem eget, ullamcorper pulvinar velit. In vel facilisis nisi, pharetra dictum dui.", PricePer1h = 500, PricePer24h = 1555, PricePer1m = 10220, PictureFileName = "6.png", DateAdded = new DateTime(2019, 12, 1), SerieId = 2 },

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
            const string name = "admin@jcpcars.com";
            const string password = "Admin123$";
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