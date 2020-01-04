using JCPCars.Migrations;
using JCPCars.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                new Serie() {SerieId = 1, Name = "Hot-hatch", IconFilename = "hot-hatch.png"},
                new Serie() { SerieId = 2, Name = "Limuzyny", IconFilename = "limuzyny.png" },
                new Serie() { SerieId = 3, Name = "Kombi", IconFilename = "kombi.png" },
                new Serie() { SerieId = 4, Name = "Cabrio", IconFilename = "cabrio.png" },
                new Serie() { SerieId = 5, Name = "Supercars", IconFilename = "supercars.png" }
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
    }
}