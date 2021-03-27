using ASonline.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASonline.ViewModels
{
    public class ProductSearchViewModels
    {
        public int? PageNo { get; set; }
        public List<Product> Products { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    public class ProductImage
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }

    public class NewProductViewModel
    {
        [Required]
        [MinLength(5), MaxLength(50)]
        public String Name { get; set; }

        [MaxLength(1000)]
        public String Description { get; set; }

        [Range(1, 1000000000000000)]
        public int PriceUnderline { get; set; }

        [Required]
        [Range(1, 1000000000000000)]
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public List<ProductImage> ProductImage { get; set; }
        public List<ProductStock> Stock { get; set; }
        public List<Category> AvailableCategories { get; set; }
    }

    public class EditProductViewModel
    {

        public int Id { get; set; }

        [Required]
        [MinLength(5), MaxLength(50)]
        public String Name { get; set; }

        [MaxLength(1000)]
        public String Description { get; set; }

        [Range(1, 1000000000000000)]
        public int PriceUnderline { get; set; }

        [Required]
        [Range(1, 1000000000000000)]
        public int Price { get; set; }

        public int CategoryId { get; set; }
        public String ImageUrl { get; set; }

        public List<Category> AvailableCategories { get; set; }
    }

    public class SizeProductViewModel
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public int Quantity { get; set; }
    }

    public class ProductViewModel
    {
        public Product Product { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }

}