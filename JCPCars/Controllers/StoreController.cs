using JCPCars.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JCPCars.Controllers
{
    public class StoreController : Controller
    {
        StoreContext db = new StoreContext();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Details(int id)
        {
            var car = db.Cars.Find(id);

            return View(car);
        }

        public ActionResult List(string seriename)
        {
            var serie = db.Series.Include("Cars").Where(g => g.Name.ToUpper() == seriename.ToUpper()).Single();
            var cars = serie.Cars.ToList();
            return View(cars);
        }

        [ChildActionOnly]
        public ActionResult SeriesMenu()
        {
            var series = db.Series.ToList();

            return PartialView("_SeriesMenu", series);
        }
        [ChildActionOnly]
        public ActionResult SeriesMenu2()
        {
            var series = db.Series.ToList();

            return PartialView("_SeriesMenu2", series);
        }
    }
}