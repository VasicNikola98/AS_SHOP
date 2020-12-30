using System;
using System.Collections.Generic;
using System.Text;

namespace ASonline.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderedAt { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
        public virtual OrderDetails OrderDetail { get; set; }

    }
}
