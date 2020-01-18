using JCPCars.DAL;
using JCPCars.Models;
using JCPCars.ViewModels;
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
            var series = db.Series.ToList();
            var newArrivals = db.Cars.OrderByDescending(a => a.DateAdded).Take(3).ToList();

            var vm = new HomeViewModel()
            {
                Series = series,
                NewArrivals = newArrivals
            };

            return View(vm);
        }

        public ActionResult StaticContent(string viewname)
        {
            return View(viewname);
        }
    }
}