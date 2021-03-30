using System;
using System.Collections.Generic;
using System.Text;

namespace ASonline.Entities
{
    public class Newsletter
    {
        public int Id { get; set; }
        public string NewsletterEmail { get; set; }
        public DateTime SubscribedAt { get; set; }
        public bool IsVerified { get; set; }
    }
}
