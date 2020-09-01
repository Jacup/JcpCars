using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JCPCars.Models
{
    public class CartItem
    {
        public Car Car { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}