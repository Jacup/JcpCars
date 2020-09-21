using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JCPCars.Models
{
    public class MessageViewModel
    {
        public Message Message { get; set; }
        public IEnumerable<Car> Cars { get; set; }
        public bool? ConfirmSuccess { get; set; }

    }
}