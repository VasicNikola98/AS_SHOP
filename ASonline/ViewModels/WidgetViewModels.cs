using ASonline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASonline.ViewModels
{
    public class ProductWidgetViewModel
    {
        public List<Product> Products { get; set; }
        public bool IsLatestProducts { get; set; }
    }
}