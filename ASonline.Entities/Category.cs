using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASonline.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MinLength(3), MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public bool IsFeatured { get; set; }
        public List<Product> Products { get; set; }
    }
}
