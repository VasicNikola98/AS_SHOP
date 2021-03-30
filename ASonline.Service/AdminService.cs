using AS.Database;
using ASonline.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                return ctx.Orders.OrderByDescending(x => x.OrderedAt).Take(n).ToList();
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
