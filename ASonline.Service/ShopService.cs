using AS.Database;
using ASonline.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ASonline.Service
{
    public class ShopService
    {
        #region Singleton
        public static ShopService Instance
        {
            get
            {
                if (instance == null) instance = new ShopService();

                return instance;
            }
        }

        private static ShopService instance { get; set; }

        private ShopService()
        {

        }
        #endregion

        #region Order
        public int SaveOrder(Order order)
        {
            using (var ctx = new ASDbContext())
            {
                ctx.Orders.Add(order);
                return ctx.SaveChanges();
            }
        }

        public void SaveOrderDetails(OrderDetails orderDetails)
        {
            using (var ctx = new ASDbContext())
            {

                ctx.OrderDetails.Add(orderDetails);
                ctx.SaveChanges();

            }
        }
        #endregion

        #region CartItem
        public bool SaveCartItem(CartItem item)
        {
            using (var ctx = new ASDbContext())
            {
                var cartItem = ctx.CartItems.Where(x =>x.HashUserId == item.HashUserId 
                        && x.Product.Id == item.Product.Id 
                        && x.Size == item.Size)
                        .FirstOrDefault();

                var maxQuantity = item.Product
                    .ProductStocks.Where(x => x.Size == item.Size)
                    .FirstOrDefault();

                if (cartItem == null && item.Quantity > 0 && item.Quantity <= maxQuantity.Quantity)
                {
                    ctx.Entry(item.Product).State = System.Data.Entity.EntityState.Unchanged;
                    ctx.CartItems.Add(item);
                    ctx.SaveChanges();
                    return true;
                }
                else if (cartItem != null && item.Quantity > 0 && (cartItem.Quantity + item.Quantity) > maxQuantity.Quantity)
                {
                    return false;
                }
                else if(item.Quantity <= 0 ||  item.Quantity > maxQuantity.Quantity)
                {
                    return false;
                }
                else
                {
                    cartItem.Quantity += item.Quantity;
                    ctx.Entry(cartItem.Product).State = System.Data.Entity.EntityState.Unchanged;
                    ctx.Entry(cartItem).State = System.Data.Entity.EntityState.Modified;
                    ctx.SaveChanges();
                    return true;
                }
            }
        }

        public List<CartItem> GetCartItemsByHashId(string HashId)
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.CartItems.Where(x => x.HashUserId.Contains(HashId))
                    .Include(x => x.Product)
                    .Include("Product.ProductImages")
                    .ToList();
            }
        }

        public void DeleteCartItem(string HashId, int ProductId)
        {
            using (var ctx = new ASDbContext())
            {
                if (!string.IsNullOrEmpty(HashId))
                {
                    var myCart = ctx.CartItems.Where(x => x.HashUserId.Contains(HashId)).ToList();
                    var cartItem = myCart.Where(x => x.Id == ProductId).FirstOrDefault();
                    ctx.CartItems.Remove(cartItem);
                    ctx.SaveChanges();
                }
               
            }
        }

        public void DeleteCartItems(string HashId)
        {
            using (var ctx = new ASDbContext())
            {
                if (!string.IsNullOrEmpty(HashId))
                {
                    var myCart = ctx.CartItems.Where(x => x.HashUserId.Contains(HashId)).ToList();
                    ctx.CartItems.RemoveRange(myCart);
                    ctx.SaveChanges();
                }

            }
        }
        #endregion

        #region Newsletter
        public void SaveNewsletter(Newsletter newsletter)
        {
            using (var ctx = new ASDbContext())
            {
                if(newsletter != null)
                {
                    ctx.Newsletters.Add(newsletter);
                    ctx.SaveChanges();
                }
            }
        }

        public List<Newsletter> GetNewsletters(bool verified)
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Newsletters.Where(x => x.IsVerified == verified).ToList();
            }
        }

        public List<Newsletter> GetLatestNewsletters(int n)
        {
            using (var ctx = new ASDbContext())
            {
                return ctx.Newsletters.Take(n).ToList();
            }
        }

        public void AcceptNewsletter(int Id)
        {
            using (var ctx = new ASDbContext())
            {
                var subscriber = ctx.Newsletters.Where(x => x.Id == Id).FirstOrDefault();
                subscriber.IsVerified = true;

                ctx.Entry(subscriber).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }

        public void DeclineNewsletter(int Id)
        {
            using (var ctx = new ASDbContext())
            {
                var subscriber = ctx.Newsletters.Where(x => x.Id == Id).FirstOrDefault();
                
                ctx.Newsletters.Remove(subscriber);
                ctx.SaveChanges();
            }
        }
        #endregion

        public void UpdateStockProduct(List<CartItem> cartItem)
        {
            using (var ctx = new ASDbContext())
            {
                if (cartItem.Count != 0)
                {
                    foreach (var item in cartItem)
                    {
                        var getItem = ctx.ProductStocks.Where(x => x.Product.Id == item.Product.Id && x.Size == item.Size)
                            .FirstOrDefault();
                        getItem.Quantity -= item.Quantity;
                        ctx.Entry(getItem.Product).State = System.Data.Entity.EntityState.Unchanged;
                        ctx.Entry(getItem).State = System.Data.Entity.EntityState.Modified;
                    }
                    ctx.SaveChanges();
                }
            }
        }
    }
}
