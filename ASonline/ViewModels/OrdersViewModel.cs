using ASonline.Entities;
using ASonline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASonline.ViewModels
{

    public class OrdersViewModel
    {
        public string Status { get; set; }
        public List<Order> Orders { get; set; }
        public Pager Pager { get; set; }
        public int InProgressCount { get; set; }
        public int DeliveredCount { get; set; }
        public int UnresolvedCount { get; set; }
        public string SearchTerm { get; set; }

    }

    public class OrderDetailsViewModel
    {
        public Order Order { get; set; }
        public List<string> AvailableStatues { get; set; }
    }
}