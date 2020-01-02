using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JCPCar.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public int SerieId { get; set; }

        [Display(Name = "Model")]
        [Required]
        public string CarModel { get; set; }

        [Display(Name = "Marka")]
        [DataType(DataType.Text)]
        [Required]
        public string CarBrand { get; set; }
        public DateTime DateAdded { get; set; }
        public string PictureFileName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

//      public virtual Serie Serie { get; set; }
        public bool IsHidden { get; set; }
    }
}