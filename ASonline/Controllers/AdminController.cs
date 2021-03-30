using ASonline.Service;
using ASonline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASonline.Controllers
{
    [Authorize(Roles = "Admin")]
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
            model.LatestNewsletter = ShopService.Instance.GetLatestNewsletters(5);

            return View(model);
        }

        #region Newsletter
        public ActionResult NewsletterIndex()
        {
            return View();
        }

        public ActionResult NewSletter()
        {
            var model = new NewslatterViewModel();
            model.Newsletters = ShopService.Instance.GetNewsletters(false);

            return View(model);
        }

        public ActionResult AcceptNewsletter(int Id)
        {

            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if(Id > 0)
            {
                ShopService.Instance.AcceptNewsletter(Id);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }
            return RedirectToAction("NewSletter", "Admin");

        }

        public ActionResult DeclineNewsletter(int Id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (Id > 0)
            {
                ShopService.Instance.DeclineNewsletter(Id);
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }
            return RedirectToAction("NewSletter","Admin");

        }
        #endregion
    }
}