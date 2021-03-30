using AS.Database;
using ASonline.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ASonline.Service
{
    public class ProductService
    {
        #region Singleton
        public static ProductService Instance
        {
            get
            {
                if (instance == null) instance = new ProductService();

                return instance;
            }
        }

        private static ProductService instance { get; set; }

        private ProductService()
        {

        }
        #endregion

        #region Product
        public void SaveProduct(Product product)
        {
            using (var ctx = new ASDbContext())
            {
                if (product != null)
                {
                    ctx.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;
                    ctx.Products.Add(product);
                    ctx.SaveChanges();
                }
            }
        }
        public List<Product> GetProducts(string search, int pageNo, int pageSize)
        {
            using (var ctx = new ASDbContext())
            {

                if (!String.IsNullOrEmpty(search))
                {

                    return ctx.Products.Where(p => p.Name != null && p.Name.ToLower()
                    .Contains(search.ToLower()))
                    .OrderBy(x => x.Id)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.Category)
                    .Include(x => x.Reviews)
                    .Include(x => x.ProductImages)
                    .OrderByDescending(x => x.Price)
                    .ToList();

                }
                else
                {
                    return ctx.Products.OrderBy(x => x.Id)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.ProductStocks)
                        .Include(x => x.Category)
                        .Include(x => x.Reviews)
                        .OrderByDescending(x => x.Price)
                        .ToList();
                }

            }
        }
        public List<Product> GetProducts(int pageNo, int pageSize)
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Products
                    .OrderByDescending(x => x.Id)
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .Include(x => x.ProductStocks)
                    .Include(x => x.ProductImages)
                    .Include(x => x.Category)
                    .Include(x => x.Reviews)
                    .OrderByDescending(x => x.Price)
                    .ToList();
            }
        }
        public List<Product> GetProductsByCategory(int categoryId, int pageSize)
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Products
                    .Where(x => x.Category.Id == categoryId)
                    .OrderByDescending(x => x.Id)
                    .Take(pageSize)
                    .Include(x => x.ProductStocks)
                    .Include(x => x.ProductImages)
                    .Include(x => x.Category)
                    .Include(x => x.Reviews)
                    .ToList();
            }
        }
        public List<Product> GetProductsByCategory(int categoryId)
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Products
                    .Where(x => x.Category.Id == categoryId)
                    .Include(x => x.ProductStocks)
                    .Include(x => x.ProductImages)
                    .Include(x => x.Reviews)
                    .OrderByDescending(x => x.Id)
                    .ToList();
            }
        }
        public List<Product> GetLatestProducts(int numberOfProducts)
        {

            using (var context = new ASDbContext())
            {
                return context.Products
                    .OrderByDescending(x => x.Id)
                    .Take(numberOfProducts)
                    .Include(x => x.ProductStocks)
                    .Include(x => x.ProductImages)
                    .Include(x => x.Category)
                    .Include(x => x.Reviews)
                    .ToList();
            }
        }
        public List<Product> GetProducts()
        {
            using (var ctx = new ASDbContext())
            {
                var products = ctx.Products
                    .Include(x => x.ProductStocks)
                    .Include(x => x.ProductImages)
                    .Include(x => x.Category)
                    .Include(x => x.Reviews)
                    .OrderByDescending(x => x.Price)
                    .ToList();

                return products;
            }
        }
        public int GetProductsCount(string search)
        {
            using (var ctx = new ASDbContext())
            {
                if (!String.IsNullOrEmpty(search))
                {
                    return ctx.Products
                        .Where(p => p.Name != null && p.Name.ToLower()
                        .Contains(search.ToLower()))
                        .OrderBy(x => x.Id)
                        .Count();
                }
                else
                {
                    return ctx.Products.Count();
                }

            }
        }
        public Product GetProductById(int id)
        {

            using (var ctx = new ASDbContext())
            {
                var product = ctx.Products
                    .Where(x => x.Id == id)
                    .Include(x => x.ProductStocks)
                    .Include(x => x.ProductImages)
                    .Include(x => x.Category)
                    .Include(x => x.Reviews)
                    .FirstOrDefault();

                return product;
            }

        }
        public int GetMaximumPrice()
        {
            using (var ctx = new ASDbContext())
            {
                return (int)(ctx.Products.Max(x => x.Price));
            }
        }
        public List<Product> SearchProducts(string searchTerm, int? categoryId, int? sortBy, int pageNo, int pageSize)
        {
            using (var ctx = new ASDbContext())
            {
                var products = ctx.Products
                    .Include(x => x.ProductStocks)
                    .Include(x => x.ProductImages)
                    .Include(x => x.Reviews)
                    .OrderByDescending(x => x.Price)
                    .ToList();

                if (categoryId.HasValue)
                {
                    products = products
                        .Where(x => x.Category.Id == categoryId.Value)
                        .ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products
                        .Where(x => x.Name.ToLower()
                        .Contains(searchTerm.ToLower()))
                        .ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        case 4:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Id).ToList();
                            break;
                    }
                }

                return products.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public int SearchProductsCount(string searchTerm, int? categoryId, int? sortBy)
        {
            using (var ctx = new ASDbContext())
            {
                var products = ctx.Products
                    .Include(x => x.ProductStocks)
                    .Include(x => x.Reviews)
                    .ToList();

                if (categoryId.HasValue)
                {
                    products = products
                        .Where(x => x.Category.Id == categoryId.Value)
                        .ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products
                        .Where(x => x.Name.ToLower()
                        .Contains(searchTerm.ToLower()))
                        .ToList();
                }

                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {
                        case 2:
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        case 4:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Id).ToList();
                            break;
                    }
                }

                return products.Count;
            }
        }
        public List<Product> GetProductById(List<int> ids)
        {

            using (var ctx = new ASDbContext())
            {
                return ctx.Products
                    .Where(x => ids.Contains(x.Id))
                    .Include(x => x.ProductStocks)
                    .Include(x => x.ProductImages)
                    .Include(x => x.Reviews)
                    .Include(x => x.ProductImages)
                    .ToList();
            }

        }
        public void UpdateProduct(Product product)
        {
            using (var ctx = new ASDbContext())
            {
                var productInDb = ctx.Products.Where(x => x.Id == product.Id).FirstOrDefault();
                productInDb.Name = product.Name;
                productInDb.Description = product.Description;
                productInDb.PriceUnderline = product.PriceUnderline;
                productInDb.Price = product.Price;
                productInDb.Category = product.Category;

             

                ctx.Entry(productInDb.Category).State = System.Data.Entity.EntityState.Unchanged;
                ctx.Entry(productInDb).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
        public void DeleteProduct(Product product)
        {
            using (var ctx = new ASDbContext())
            {
                var deleteProduct = ctx.Products
                    .Where(x => x.Id == product.Id)
                    .Include(x => x.ProductStocks)
                    .Include(x => x.ProductImages)
                    .Include(x => x.Reviews)
                    .Include(x => x.CartItems)
                    .FirstOrDefault();

                if (deleteProduct != null)
                {
                    ctx.ProductImages.RemoveRange(deleteProduct.ProductImages);
                    ctx.ProductStocks.RemoveRange(deleteProduct.ProductStocks);
                    ctx.Reviews.RemoveRange(deleteProduct.Reviews);
                    ctx.CartItems.RemoveRange(deleteProduct.CartItems);
                    ctx.Products.Remove(deleteProduct);
                    ctx.SaveChanges();
                }
            }
        }
        public void SaveSize(ProductStock stock)
        {
            using (var ctx = new ASDbContext())
            {
                var myProductStock = ctx.ProductStocks.Where(x => x.Product.Id == stock.Product.Id && x.Size == stock.Size).FirstOrDefault();

                if(myProductStock == null)
                {
                    ctx.Entry(stock.Product).State = System.Data.Entity.EntityState.Unchanged;
                    ctx.ProductStocks.Add(stock);
                    ctx.SaveChanges();
                }
                else
                {
                    ctx.Entry(myProductStock.Product).State = System.Data.Entity.EntityState.Unchanged;
                    myProductStock.Quantity += stock.Quantity;
                    ctx.Entry(myProductStock).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
        }
        public void DeleteSize(int Id)
        {
            using (var ctx = new ASDbContext())
            {
                var size = ctx.ProductStocks.Where(x => x.Id == Id).FirstOrDefault();
                ctx.ProductStocks.Remove(size);
                ctx.SaveChanges();
            }
        }
        #endregion

        #region Review
        public void SaveReview(Review review)
        {
            using (var ctx = new ASDbContext())
            {
                try
                {
                    ctx.Entry(review.Product).State = System.Data.Entity.EntityState.Unchanged;
                    ctx.Reviews.Add(review);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public List<Review> GetReviews()
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Reviews
                    .Where(x => x.isConfirmed == false)
                    .Include(x => x.Product)
                    .ToList();
            }
        }
        public Review GetReviewById(int Id)
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Reviews
                    .Where(x => x.Id == Id)
                    .Include(x => x.Product)
                    .FirstOrDefault();      
            }
        }
        public void UpdateReview(Review review)
        {
            using (var ctx = new ASDbContext())
            {
                ctx.Entry(review.Product).State = System.Data.Entity.EntityState.Unchanged;
                ctx.Entry(review).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
        public void DeleteReview(int Id)
        {
            using (var ctx = new ASDbContext())
            {
                if(Id > 0)
                {
                    var review = ctx.Reviews.Where(x => x.Id == Id).FirstOrDefault();
                    ctx.Reviews.Remove(review);
                    ctx.SaveChanges();
                }
            }
        }
        #endregion

    }
}
