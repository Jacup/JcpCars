using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace JCPCars.Validators
{
    public class PriceAttribute : ValidationAttribute
    {
        private readonly int _MinPrice = 0;
        
        public PriceAttribute(int minPrice)
        {
            _MinPrice = minPrice;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                int data = (int)value;
                if (data < _MinPrice)
                {
                    return new ValidationResult("Cena musi być wyższa niż 50 PLN.");
                }
            }
            return ValidationResult.Success;
        }
    }

    public class PriceValidator : DataAnnotationsModelValidator<PriceAttribute>
    {
        int minPrice = 0;
        string errorMsg = string.Empty;

        public PriceValidator(ModelMetadata metadata, ControllerContext context, PriceAttribute attribute)
            : base(metadata,context,attribute)
        {
            errorMsg = attribute.ErrorMessage;
        }

        public override IEnumerable<ModelClientValidationRule>
            GetClientValidationRules()
        {
            ModelClientValidationRule validationRule = new ModelClientValidationRule();
            validationRule.ErrorMessage = errorMsg;
            validationRule.ValidationType = "Price";

            validationRule.ValidationParameters.Add("MinPrice", minPrice);

            return new[] { validationRule };
        }
    }
}