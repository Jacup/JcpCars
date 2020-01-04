using JCPCars.DAL;
using JCPCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCPCars.Controllers
{
    public class HomeController : Controller
    {
        private StoreContext db = new StoreContext();

        public ActionResult Index()
        {
            //Serie newSerie = new Serie { Name = "Cabrio", Description = " Opis kategorii", IconFilename = "1.png" };
            //db.Series.Add(newSerie);
            //db.SaveChanges();

            var seriesList = db.Series.ToList();

            return View();
        }

        public ActionResult StaticContent(string viewname)
        {
            return View(viewname);
        }
    }
}