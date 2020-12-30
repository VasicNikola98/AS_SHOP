using ASonline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASonline.ViewModels
{
    public class ReviewViewModel
    {
        public int Rating { get; set; }
        public string Content { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public DateTime Created { get; set; }
        public bool isConfirmed { get; set; }
        public Product Product { get; set; }
    }

    public class ReviewViewListingModel
    {
        public List<Review> Reviews { get; set; }
    }

    public class NewReviewViewModel
    {
        public int productId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }

}