using System;
using System.Collections.Generic;
using System.Text;

namespace ASonline.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
       
        public string Size { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public int PriceUnderline { get; set; }
        public virtual Order Order { get; set; }


    }
}
