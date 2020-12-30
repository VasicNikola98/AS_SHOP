using System;
using System.Collections.Generic;
using System.Text;

namespace ASonline.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
        public bool isConfirmed { get; set; }
        public virtual Product Product { get; set; }

    }
}
    