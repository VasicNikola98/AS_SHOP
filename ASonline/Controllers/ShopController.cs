using ASonline.Code;
using ASonline.Entities;
using ASonline.Service;
using ASonline.ViewModels;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ASonline.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult Index(string searchTerm, int? categoryId, int? sortBy, int? pageNo)
        {
            ShopViewModel model = new ShopViewModel();

            model.FeaturedCategories = CategoryService.Instance.GetFeaturedCategories();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SearchTerm = searchTerm;
            model.SortBy = sortBy;
            model.CategoryId = categoryId;
            

            var totalCount = ProductService.Instance.SearchProductsCount(searchTerm ,categoryId, sortBy);
            model.Products = ProductService.Instance.SearchProducts(searchTerm ,categoryId, sortBy, pageNo.Value, 9);
            model.Pager = new Pager(totalCount, pageNo, 9);
            return View(model);
        }
        public ActionResult FilterProducts(string searchTerm, int? categoryId, int? sortBy, int? pageNo)
        {
            FilterProductaViewModel model = new FilterProductaViewModel();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            model.SortBy = sortBy;
            model.CategoryId = categoryId;
            model.SearchTerm = searchTerm;
            model.FeaturedCategories = CategoryService.Instance.GetFeaturedCategories();
           

            var totalCount = ProductService.Instance.SearchProductsCount(searchTerm, categoryId, sortBy);
            model.Products = ProductService.Instance.SearchProducts(searchTerm, categoryId, sortBy, pageNo.Value, 9);
            model.Pager = new Pager(totalCount, pageNo, 9);

            return PartialView(model);
        }
        public JsonResult PlaceCartItem(PlaceCartItemViewModel model)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var HashedUserId = Request.Cookies["cartItemHashedUserId"];

            if(HashedUserId != null && !string.IsNullOrEmpty(HashedUserId.Value))
            {
                
                var cartItem = new CartItem();
                cartItem.HashUserId = HashedUserId.Value;
                cartItem.Size = model.productSize;
                cartItem.Quantity = int.Parse(model.productQuantity);
                var cartProduct = ProductService.Instance.GetProductById(int.Parse(model.productId));
                cartItem.Product = cartProduct;

                ShopService.Instance.SaveCartItem(cartItem);

                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }

            return result;
        }
        [Route("Checkout")]
        public ActionResult Checkout()
        {
            CheckoutViewModels model = new CheckoutViewModels();

            var HashedUserId = Request.Cookies["cartItemHashedUserId"];

            if (HashedUserId != null && !string.IsNullOrEmpty(HashedUserId.Value))
            {
                model.CartProducts = ShopService.Instance.GetCartItemsByHashId(HashedUserId.Value);

            }

           return View(model);
        }
        public JsonResult PlaceOrder(PlaceOrderViewModel model)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            var HashedUserId = Request.Cookies["cartItemHashedUserId"];

            if (HashedUserId != null && !string.IsNullOrEmpty(HashedUserId.Value))
            {
                Order newOrder = new Order();
                OrderDetails orderDetails = new OrderDetails();

                orderDetails.FirstName = model.FirstName;
                orderDetails.LastName = model.LastName;
                orderDetails.Email = model.Email;
                orderDetails.Address = model.Address;
                orderDetails.Country = model.Country;
                orderDetails.City = model.City;
                orderDetails.Nummber = model.Nummber;
                orderDetails.PostCode = model.PostCode;

                ShopService.Instance.SaveOrderDetails(orderDetails);

                var boughtProducts = ShopService.Instance.GetCartItemsByHashId(HashedUserId.Value);
               
                newOrder.OrderedAt = DateTime.Now;
                newOrder.Status = "Nerešena";
                newOrder.TotalAmount = boughtProducts.Sum(x => x.Product.Price * x.Quantity);
                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem() { ProductId = x.Product.Id, Quantity = x.Quantity, Size = x.Size }));
                newOrder.OrderDetail = orderDetails;

                ShopService.Instance.DeleteCartItems(HashedUserId.Value);

                var rowsEffected = ShopService.Instance.SaveOrder(newOrder);
                result.Data = new { Success = true, Rows = rowsEffected };
            }
            else
            {
                result.Data = new { Success = false };
            }

            return result;
        }
        public ActionResult DeleteCartItem(int Id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            var HashedUserId = Request.Cookies["cartItemHashedUserId"];

            if (HashedUserId != null && !string.IsNullOrEmpty(HashedUserId.Value))
            {
                ShopService.Instance.DeleteCartItem(HashedUserId.Value, Id);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }
           
            return RedirectToAction("Checkout","Shop");
        }
        public JsonResult GetCartCounter()
        {
            var HashedUserId = Request.Cookies["cartItemHashedUserId"];
            var productCounter = 0;
            if (HashedUserId != null && !string.IsNullOrEmpty(HashedUserId.Value))
            {
                var product = ShopService.Instance.GetCartItemsByHashId(HashedUserId.Value);
                foreach(var item in product)
                {
                    productCounter += item.Quantity;
                }
            }
            else
            {
                productCounter = 0;
            }

            var result = new JsonResult { Data = productCounter, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return result;
        }

        [HttpPost]
        public JsonResult AddNewsletter(string NewsletterEmail)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if(!string.IsNullOrEmpty(NewsletterEmail))
            {
                Newsletter newsletter = new Newsletter();

                newsletter.NewsletterEmail = NewsletterEmail;
                ShopService.Instance.SaveNewsletter(newsletter);

                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }
            return result;
        }
    }
    
}