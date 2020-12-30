using System;
using System.Collections.Generic;
using System.Text;

namespace ASonline.Entities
{
    public class BillingAddress
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Nummber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
    }
}
