using AS.Database;
using ASonline.Entities;
using System.Collections.Generic;
using System.Linq;
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



        #region GET
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
        public int GetOrderCount(string status,bool IsArhive)
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Orders
                    .Where(x => x.Status.Contains(status) && x.IsArhivated == IsArhive)
                    .Count();
            }
        }
      
        #endregion

        #region Search
        public int SearchOrdersCount(string searchTerm, string status)
        {
            using (var ctx = new ASDbContext())
            {
                var orders = ctx.Orders
                    .Include(x => x.OrderDetail)
                    .OrderByDescending(x => x.OrderedAt)
                    .ToList();

                if (!string.IsNullOrEmpty(searchTerm))
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
        public List<Order> SearchOrders(string searchTerm, string status, int pageNo, int pageSize, bool IsArhive)
        {
            using (var ctx = new ASDbContext())
            {
                var orders = ctx.Orders
                    .Where(x => x.IsArhivated == IsArhive)
                    .Include(x => x.OrderDetail)
                    .OrderByDescending(x => x.OrderedAt)
                    .ToList();

                if (!string.IsNullOrEmpty(searchTerm))
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
        #endregion

        #region Update
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
        #endregion

        public bool ArhiveOrder(int id)
        {
            using (var ctx = new ASDbContext())
            {
                var order = ctx.Orders.Where(x => x.Id == id).FirstOrDefault();
                
                if(order.Status == "Isporučena" && order != null)
                {
                    order.IsArhivated = true;
                    ctx.Entry(order).State = EntityState.Modified;
                    return ctx.SaveChanges() > 0;
                }
                else
                {
                    return false;
                }
              
            }
        }

        public bool RecoverOrder(int id)
        {
            using (var ctx = new ASDbContext())
            {
                var order = ctx.Orders.Where(x => x.Id == id).FirstOrDefault();

                if (order != null)
                {
                    order.IsArhivated = false;
                    ctx.Entry(order).State = EntityState.Modified;
                    return ctx.SaveChanges() > 0;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}
