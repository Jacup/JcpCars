using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JCPCars.Models
{
    public class Message
    {
        public int MessageId { get; set; }

        public string ToUserId { get; set; }
        public string FromUserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public string Content { get; set; }

        public DateTime Created { get; set; }
    }
}