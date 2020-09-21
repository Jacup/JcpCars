using JCPCars.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JCPCars.Models
{
    public class Message
    {
        //misc
        public int MessageId { get; set; }
        public DateTime Created { get; set; }


        //data
        public string Content { get; set; }


        //users
        public string ToUserId { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        //car
        public int CarId { get; set; }
        public virtual Car Car { get; set; }
    }
}