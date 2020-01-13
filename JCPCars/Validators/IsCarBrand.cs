using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JCPCars.Validators
{
    public class IsCarBrand : ValidationAttribute
    {
        public string Brand { get; private set; }

        public IsCarBrand()
        {
            Brand = null;
        }

        public IsCarBrand(string brand)
        {
            Brand = brand;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ValidationResult.Success;
        }
    }
}