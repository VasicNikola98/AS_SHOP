using ASonline.Entities;
using ASonline.Service;
using ASonline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Data.Entity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace ASonline.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

   

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ProductTable(string search, int? pageNo)
        { 
            ProductSearchViewModels model = new ProductSearchViewModels();
            model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = ProductService.Instance.GetProductsCount(search);
            model.SearchTerm = search;
           
            model.Products = ProductService.Instance.GetProducts(search,pageNo.Value, 5);
          
            if (model.Products != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, 5);

                return PartialView("ProductTable", model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpGet]
        [Route("Details/{Id}")]
        public ActionResult Details(int Id)
        {
            ProductViewModel model = new ProductViewModel();

            model.Product = ProductService.Instance.GetProductById(Id);

            if(User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = UserManager.FindById(userId);
                model.Email = user.Email;
                model.Name = user.Name;
            }
            else
            {
                model.Email = "";
                model.Name = "";
            }

            if (model.Product != null)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Size(int Id)
        {
            SizeProductViewModel model = new SizeProductViewModel();

            var product = ProductService.Instance.GetProductById(Id);
            model.Product = product;
            model.ProductId = Id;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Size(SizeProductViewModel model)
        {
            var stock = new ProductStock();

            stock.Size = model.Size;
            stock.Quantity = model.Quantity;
            
            switch(stock.Size)
            {
                case "XS":
                    stock.DefaultWeight = 0;
                    break;
                case "S":
                    stock.DefaultWeight = 1;
                    break;
                case "M":
                    stock.DefaultWeight = 2;
                    break;
                case "L":
                    stock.DefaultWeight = 3;
                    break;
                case "XL":
                    stock.DefaultWeight = 4;
                    break;
                case "XXL":
                    stock.DefaultWeight = 5;
                    break;
                case "XXXL":
                    stock.DefaultWeight = 6;
                    break;
                default:
                    break;
            }

            stock.Product = ProductService.Instance.GetProductById(model.ProductId);

            ProductService.Instance.SaveSize(stock);

            return RedirectToAction("ProductTable");
        }


        #region Creation
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            NewProductViewModel model = new NewProductViewModel();

            model.AvailableCategories = CategoryService.Instance.GetCategories();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(NewProductViewModel model)
        {
                var product = new Product();
                product.Name = model.Name;
                product.Description = model.Description;
                product.PriceUnderline = model.PriceUnderline;
                product.Price = model.Price;
                product.Category = CategoryService.Instance.GetCategoryById(model.CategoryId);

               
                var s = model.ImageUrl.Split(',');
                
                product.ProductImages = new List<ProductImages>();
                for (var i = 0; i < s.Length; i++)
                {
                    product.ProductImages.Add(new ProductImages { ImageURL = s[i] });
                }
                
                
            

                ProductService.Instance.SaveProduct(product);

                return RedirectToAction("ProductTable");
            
        }
        #endregion

        #region Updation
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int Id)
        {
            EditProductViewModel model = new EditProductViewModel();

            var product = ProductService.Instance.GetProductById(Id);

            model.Id = product.Id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.PriceUnderline = product.PriceUnderline;
            model.Price = product.Price;
            model.CategoryId = product.Category != null ? product.Category.Id : 0;
            //model.ImageUrl = product.ImageUrl;
            model.AvailableCategories = CategoryService.Instance.GetCategories();

            return PartialView(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = ProductService.Instance.GetProductById(model.Id);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.PriceUnderline = model.PriceUnderline;
            existingProduct.Price = model.Price;
            existingProduct.Category = CategoryService.Instance.GetCategoryById(model.CategoryId);
           // existingProduct.ImageUrl = model.ImageUrl;

           
            ProductService.Instance.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }
        #endregion

        #region Deletion
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Product product)
        {

            if (product != null)
            {
                ProductService.Instance.DeleteProduct(product);
            }

            return RedirectToAction("ProductTable");
        }
        #endregion

        #region Review
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Reviews()
        {
            var model = new ReviewViewListingModel();
            model.Reviews = ProductService.Instance.GetReviews();

            return View(model);
        }

        [HttpPost]
        public JsonResult AddReviews(NewReviewViewModel model)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (model != null)
            {
                var product = ProductService.Instance.GetProductById(model.productId);

                Review review = new Review();
                review.Username = model.Name;
                review.Email = model.Email;
                review.Rating = model.Rating;
                review.Created = DateTime.Now;
                review.isConfirmed = false;
                review.Content = model.Comment;
                review.Product = product;

                ProductService.Instance.SaveReview(review);

                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }

            return result;
        }
      
        [HttpPost]
        [Authorize(Roles ="Admin")]
        public JsonResult AcceptReview(int Id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if(Id > 0)
            {
                var review = ProductService.Instance.GetReviewById(Id);
                review.isConfirmed = true;
                ProductService.Instance.UpdateReview(review);
                result.Data = new { Success = true };

            }
            else
            {
                result.Data = new { Success = false };
            }

            return result;
        }

        [Authorize(Roles ="Admin")]
        public ActionResult DeclineReview(int Id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if(Id > 0)
            {
                ProductService.Instance.DeleteReview(Id);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }

            return RedirectToAction("Reviews","Product");
        }

        #endregion
    }
}