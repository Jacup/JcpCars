using JCPCars.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JCPCars.Models
{
    public class Car 
    {
        public int CarId { get; set; }
        public int SerieId { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }


        [Display(Name = "Model")]
        [Required(ErrorMessage = "Wprowadź model pojazdu")]
        public string CarModel { get; set; }

        [Display(Name = "Marka")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Wprowadź markę pojazdu")]
        public string CarBrand { get; set; }

        public DateTime DateAdded { get; set; }

        public string PictureFileName { get; set; }

        public string Description { get; set; }

        [Price(50)]
        public int Price { get; set; }

        public virtual Serie Serie { get; set; }
    }
}