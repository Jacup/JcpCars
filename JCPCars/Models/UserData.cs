using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCPCars.Models
{
    public class UserData
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        [RegularExpression(@"([\+]?[0-9]{2,3}[\ -]?)?[0-9]{3}[\ -]?[0-9]{3}[\ -]?[0-9]{3}", ErrorMessage = "Błędny format numeru telefonu.")]
        public string PhoneNumber { get; set; }

        [EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]
        public string Email { get; set; }


        public string Address { get; set; }
        public string AddressCity { get; set; }

        [RegularExpression(@"^[0-9]{2}[-\s]*?[0-9]{3}$", ErrorMessage = "Błędny format kodu pocztowego. Wymagany format: xx-xxx.")]
        public string CityCode { get; set; }


    }
}