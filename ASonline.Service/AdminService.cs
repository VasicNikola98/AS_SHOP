using AS.Database;
using ASonline.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ASonline.Service
{
    public class AdminService
    {
        #region Singleton
        public static AdminService Instance
        {
            get
            {
                if (instance == null) instance = new AdminService();

                return instance;
            }
        }

        private static AdminService instance { get; set; }

        private AdminService()
        {

        }
        #endregion


        #region GET
        public int GetProductsCount()
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Products.Count();
            }
        }

        public int GetCategoriesCount()
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Categories.Count();
            }
        }

        public int GetReviewsCount()
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Reviews.Count();
            }
        }

        public int GetOrdersCount()
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Orders.Count();
            }
        }

        public List<Order> GetLatestOrders(int n)
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Orders
                   .Where(x => x.IsArhivated == false)
                   .OrderByDescending(x => x.OrderedAt)
                   .Take(n)
                   .ToList();

            }
        }

        public List<Product> GetLatestProducts(int n)
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Products
                    .Include(x => x.ProductImages)
                    .OrderByDescending(x => x.CreatedAt)
                    .Take(n).ToList();
            }
        }

        public int GetNewsletterCount(string search)
        {
            using (var ctx = new ASDbContext())
            {
                if (string.IsNullOrEmpty(search))
                {
                    return ctx.Newsletters.Count();
                }
                else
                {
                    return ctx.Newsletters
                  .Where(x => x.NewsletterEmail.ToLower().Contains(search.ToLower()))
                  .Count();
                }

            }
        }

        public List<Newsletter> GetNewsletters(string search, int pageNo, int pageSize)
        {
            using (var ctx = new ASDbContext())
            {
                var newsletter = ctx.Newsletters.ToList();

               

                if (!string.IsNullOrEmpty(search))
                {
                    newsletter = newsletter.Where(x => x.NewsletterEmail.ToLower()
                    .Contains(search.ToLower()))
                        .ToList();
                }

             

                return newsletter.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        #endregion

        public void DeleteImage(int Id)
        {
            using (var ctx = new ASDbContext())
            {
                var image = ctx.ProductImages.Where(x => x.Id == Id).FirstOrDefault();
                ctx.ProductImages.Remove(image);
                ctx.SaveChanges();
            }
        }
    }
}
