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
    public class OrdersService
    {
        #region Singleton
        public static OrdersService Instance
        {
            get
            {
                if (instance == null) instance = new OrdersService();

                return instance;
            }
        }

        private static OrdersService instance { get; set; }

        private OrdersService()
        {

        }
        #endregion

        public Order GetOrderById(int id)
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Orders
                    .Where(x => x.Id == id)
                    .Include(x => x.OrderDetail)
                    .Include(x => x.OrderItems)
                    .FirstOrDefault();
            }
        }
        public int SearchOrdersCount(string searchTerm,string status)
        {
            using (var ctx = new ASDbContext())
            {
                var orders = ctx.Orders
                    .Include(x => x.OrderDetail)
                    .OrderByDescending(x => x.OrderedAt)
                    .ToList();

                if(!string.IsNullOrEmpty(searchTerm))
                {
                    orders = orders
                        .Where(x => x.OrderDetail.FirstName.ToLower().Contains(searchTerm.ToLower()))
                        .ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders
                        .Where(x => x.Status.ToLower().Contains(status.ToLower()))
                        .ToList();
                }

                return orders.Count;
            }
        }
        public List<Order> SearchOrders(string searchTerm,string status, int pageNo, int pageSize)
        {
            using (var ctx = new ASDbContext())
            {
                var orders = ctx.Orders
                    .Include(x => x.OrderDetail)
                    .OrderByDescending(x => x.OrderedAt)
                    .ToList();

                if(!string.IsNullOrEmpty(searchTerm))
                {
                    orders = orders
                        .Where(x => x.OrderDetail.FirstName.ToLower().Contains(searchTerm.ToLower()))
                        .ToList();
                }

                if (!string.IsNullOrEmpty(status))
                {
                    orders = orders
                        .Where(x => x.Status.ToLower().Contains(status.ToLower()))
                        .ToList();
                }

                return orders
                    .Skip((pageNo - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
        }
        public bool UpdateOrderStatus(int id, string status)
        {
            using (var ctx = new ASDbContext())
            {
                var order = ctx.Orders.Find(id);
                order.Status = status;
                ctx.Entry(order).State = EntityState.Modified;
                return ctx.SaveChanges() > 0;
            }
        }
        public int GetOrderInProgressCount()
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Orders
                    .Where(x => x.Status.Contains("U procesu"))
                    .Count();
            }
        }
        public int GetOrderDeliveredCount()
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Orders
                    .Where(x => x.Status.Contains("Isporučena"))
                    .Count();
            }
        }
        public int GetOrderUnresolvedCount()
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Orders
                    .Where(x => x.Status.Contains("Nerešena"))
                    .Count();
            }
        }

 
    }
}
