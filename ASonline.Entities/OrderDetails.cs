using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ASonline.Entities
{
    public class OrderDetails
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Nummber { get; set; }
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostCode { get; set; }

        public OrderDetails()
        {

        }

        public OrderDetails(string FirstName,string LastName, string Email, string Address, string Nummber,string Country, string City, string PostCode)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Address = Address;
            this.Nummber = Nummber;
            this.Country = Country;
            this.City = City;
            this.PostCode = PostCode;
        }
    }
}
