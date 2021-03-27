using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ASonline.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public int PriceUnderline { get; set; }

        [Required]
        [Range(1, 1000000000000000)]
        public int Price { get; set; }
        public List<ProductStock> ProductStocks { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Review> Reviews { get; set; }
        public virtual List<ProductImages> ProductImages { get; set; }

    }
}
