using ASonline.Service;
using ASonline.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASonline.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        public ActionResult Index(string searchTerm, string status, int? pageNo)
        {
            OrdersViewModel model = new OrdersViewModel();
            model.Status = status;
            model.SearchTerm = searchTerm;
            model.InProgressCount = OrdersService.Instance.GetOrderInProgressCount();
            model.DeliveredCount = OrdersService.Instance.GetOrderDeliveredCount();
            model.UnresolvedCount = OrdersService.Instance.GetOrderUnresolvedCount();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.Orders = OrdersService.Instance.SearchOrders(searchTerm,status, pageNo.Value, 5);

            var totalRecords = OrdersService.Instance.SearchOrdersCount(searchTerm,status);

            model.Pager = new Pager(totalRecords, pageNo, 5);

            return View(model);
        }

        public ActionResult Details(int id)
        {
            OrderDetailsViewModel model = new OrderDetailsViewModel();

            model.Order = OrdersService.Instance.GetOrderById(id);

            model.AvailableStatues = new List<string>() { "Nerešena", "U procesu", "Isporučena" };

            return View(model);
        }

        public JsonResult ChangeStatus(string status, int id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            result.Data = new { Success = OrdersService.Instance.UpdateOrderStatus(id, status) };

            return result;
        }
    }
}