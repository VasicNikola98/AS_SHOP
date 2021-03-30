﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ASonline.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
       
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public int PriceUnderline { get; set; }

        [Required]
        public int Price { get; set; }
        public List<ProductStock> ProductStocks { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Review> Reviews { get; set; }
        public virtual List<ProductImages> ProductImages { get; set; }
        public virtual List<CartItem> CartItems { get; set; }

    }
}
