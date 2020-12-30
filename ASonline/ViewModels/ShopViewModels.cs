using ASonline.Entities;
using ASonline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASonline.ViewModels
{
    public class CartCounterViewModel
    {
        public string HashedUserId { get; set; }
        public int CartCounter { get; set; }

    }

    public class CheckoutViewModels
    {
        public List<CartItem> CartProducts { get; set; }

    }

    public class CartItemViewModel
    {
        public string productId { get; set; }
        public string productSize { get; set; }
        public string productQuantity { get; set; }
    }

    public class ShopViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> FeaturedCategories { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryId { get; set; }
        public string SearchTerm { get; set; }
        public Pager Pager { get; set; }
    }

    public class FilterProductaViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> FeaturedCategories { get; set; }
        public Pager Pager { get; set; }
        public int? SortBy { get; set; }
        public int? CategoryId { get; set; }
        public string SearchTerm { get; set; }
    }

    public class PlaceOrderViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Nummber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }

    }

    public class PlaceCartItemViewModel
    {
        public string productId { get; set; }
        public string productSize { get; set; }
        public string productQuantity { get; set; }
    }

}