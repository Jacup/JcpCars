using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JCPCars.Models
{
    public class Serie
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int SerieId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}