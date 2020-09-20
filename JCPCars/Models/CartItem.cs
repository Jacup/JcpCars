using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JCPCars.Models
{
    public class CartItem
    {
        public Car Car { get; set; }

        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}