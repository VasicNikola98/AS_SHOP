using ASonline.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASonline.ViewModels
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }

    public class NewCategoryViewModel
    {

        [Required]
        [MinLength(3), MaxLength(50)]
        public String Name { get; set; }
        [MaxLength(1000)]
        public String Description { get; set; }
        [Required]
        public bool IsFeatured { get; set; }
        public String ImageUrl { get; set; }

      
    }

    public class EditCategoryViewModel
    {

        public int Id { get; set; }
        [Required]
        [MinLength(3), MaxLength(50)]
        public String Name { get; set; }
        [MaxLength(1000)]
        public String Description { get; set; }
        [Required]
        public bool IsFeatured { get; set; }
        public String ImageUrl { get; set; }

    }

    public class CategorySearchViewModels
    {
        public int? PageNo { get; set; }
        public List<Category> Categories { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
    }
}