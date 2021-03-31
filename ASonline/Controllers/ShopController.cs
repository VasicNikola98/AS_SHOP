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
                
                if(model.productQuantity != null)
                {
                    var cartItem = new CartItem();
                    cartItem.HashUserId = HashedUserId.Value;
                    cartItem.Size = model.productSize;
                    cartItem.Quantity = int.Parse(model.productQuantity);
                    var cartProduct = ProductService.Instance.GetProductById(int.Parse(model.productId));
                    cartItem.Product = cartProduct;
                    var validate = ShopService.Instance.SaveCartItem(cartItem);
                    if (validate)
                    {
                        result.Data = new { Success = true };
                    }
                    else
                    {
                        result.Data = new { Success = false };
                    }
                }
                else
                {
                    result.Data = new { Success = false };
                }

             
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
                OrderDetails orderDetails = new OrderDetails(model.FirstName,model.LastName,model.Email,model.Address,model.Nummber,model.Country,model.City,model.PostCode);

                var boughtProducts = ShopService.Instance.GetCartItemsByHashId(HashedUserId.Value);
               
                newOrder.OrderedAt = DateTime.Now;
                newOrder.Status = "Nerešena";

                foreach(var item in boughtProducts)
                {
                    if (item.Product.PriceUnderline > 0)
                    {
                        var procent = (double.Parse(item.Product.PriceUnderline.ToString()) / 100);
                        var salePrice = (item.Product.Price * procent);
                        var saleAmount = (item.Product.Price - salePrice);

                        newOrder.TotalAmount = newOrder.TotalAmount + saleAmount;
                       
                    }
                    else
                    {
                        newOrder.TotalAmount = newOrder.TotalAmount + (item.Product.Price * item.Quantity);
                    }
                }
             
                newOrder.OrderItems = new List<OrderItem>();
                foreach(var x in boughtProducts)
                {
                    if(x.Product.ProductImages.Count > 0)
                    {
                        newOrder.OrderItems.Add(new OrderItem
                        {
                            Quantity = x.Quantity,
                            Price = x.Product.Price,
                            PriceUnderline = x.Product.PriceUnderline,
                            Size = x.Size,
                            ProductName = x.Product.Name,
                            ImageUrl = x.Product.ProductImages[0].ImageURL
                        });
                    }
                    else
                    {
                        newOrder.OrderItems.Add(new OrderItem
                        {
                            Quantity = x.Quantity,
                            Price = x.Product.Price,
                            PriceUnderline = x.Product.PriceUnderline,
                            Size = x.Size,
                            ProductName = x.Product.Name,
                            ImageUrl = ""
                        });
                    }
                }

                newOrder.OrderDetail = orderDetails;

                ShopService.Instance.DeleteCartItems(HashedUserId.Value);

                var rowsEffected = ShopService.Instance.SaveOrder(newOrder);

                ShopService.Instance.UpdateStockProduct(boughtProducts);
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
                newsletter.SubscribedAt = DateTime.Now;
                newsletter.IsVerified = false;

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