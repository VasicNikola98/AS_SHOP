using System;
using System.Collections.Generic;
using System.Text;

namespace ASonline.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public string HashUserId { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public virtual Product Product { get; set; }
    }
}
