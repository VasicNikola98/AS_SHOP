using ASonline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASonline.ViewModels
{
    public class AdminViewIndexModel
    {
        public int CategoriesCount { get; set; }
        public int ProductsCount { get; set; }
        public int ReviewsCount { get; set; }
        public int OrdersCount { get; set; }
        public List<Order> LatestOrders { get; set; }
        public List<Product> LatestProducts { get; set; }
        public List<Newsletter> LatestNewsletter { get; set; }
    }

    public class NewslatterViewModel
    {
        public List<Newsletter> Newsletters { get; set; }
    }

    public class NewsletterFilterViewModel
    {
        public List<Newsletter> Newsletters { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
    }
}