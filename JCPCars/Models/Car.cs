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
        public DateTime DateAdded { get; set; }

        //carspecs
        [Display(Name = "Model")]
        [Required(ErrorMessage = "Wprowadź model pojazdu")]
        public string CarModel { get; set; }

        [Display(Name = "Marka")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Wprowadź markę pojazdu")]
        public string CarBrand { get; set; }

        [Display(Name = "Lokalizacja")]
        public string Location { get; set; }

        [Display(Name = "Rodzaj paliwa")]
        public string Fuel { get; set; }

        [Display(Name = "Rodzaj skrzyni biegów")]
        public string Gearbox { get; set; }

        [Display(Name = "Rok produkcji")]
        public string ProductionYear { get; set; }

        [Display(Name = "Moc (KM)")]
        public string Power { get; set; }

        //pictures
        public string PictureFileName { get; set; } //todo references down
        public string PictureFileName2 { get; set; }
        public string PictureFileName3 { get; set; }

        //cardescription
        public string Description { get; set; }


        //price
        [Price(50)]
        public int PricePer1h { get; set; }
        [Price(50)]
        public int PricePer24h { get; set; }
        [Price(50)]
        public int PricePer1m { get; set; }



        public bool IsHighlited { get; set; }

        public virtual Serie Serie { get; set; }
    }
}