using System;
using System.Collections.Generic;
using System.Text;

namespace ASonline.Entities
{
    public class ProductStock
    {
        public int Id { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public int DefaultWeight { get; set; }
        public virtual Product Product { get; set; }
    }
}
