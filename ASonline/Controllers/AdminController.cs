using ASonline.Service;
using ASonline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASonline.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            var model = new AdminViewIndexModel();
            model.CategoriesCount = AdminService.Instance.GetCategoriesCount();
            model.ProductsCount = AdminService.Instance.GetProductsCount();
            model.OrdersCount = AdminService.Instance.GetOrdersCount();
            model.ReviewsCount = AdminService.Instance.GetReviewsCount();
            model.LatestOrders = AdminService.Instance.GetLatestOrders(7);
            model.LatestProducts = AdminService.Instance.GetLatestProducts(5);
            return View(model);
        }
    }
}