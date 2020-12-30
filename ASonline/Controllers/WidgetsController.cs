using ASonline.Service;
using ASonline.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASonline.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool isLatestProducts, int? CategoryId = 0)
        {
            ProductWidgetViewModel model = new ProductWidgetViewModel();
            model.IsLatestProducts = isLatestProducts;

            if (isLatestProducts)
            {
                model.Products = ProductService.Instance.GetLatestProducts(4);
            }
            else if(CategoryId.HasValue && CategoryId.Value > 0)
            {
                model.Products = ProductService.Instance.GetProductsByCategory(CategoryId.Value,4);
            }
            else
            {
                model.Products = ProductService.Instance.GetProducts(1, 8);
            }
            return PartialView(model);
        }
    }
}