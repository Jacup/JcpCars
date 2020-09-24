using JCPCars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JCPCars.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Car> NewArrivals { get; set; }
        public IEnumerable<Serie> Series { get; set; }

        public IEnumerable<Car> Highliteds { get; set; }
    }
}