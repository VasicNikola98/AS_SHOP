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
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ArhiveIndex()
        {
            return View();
        }

        public ActionResult ArhiveOrdersTable(string searchTerm, string status, int? pageNo)
        {
            OrdersViewModel model = new OrdersViewModel();
            model.Status = status;
            model.SearchTerm = searchTerm;
          

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.Orders = OrdersService.Instance.SearchOrders(searchTerm, status, pageNo.Value, 5, true);

            var totalRecords = OrdersService.Instance.SearchOrdersCount(searchTerm, status);

            if (model.Orders != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, 5);

                return PartialView("ArhiveOrdersTable", model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public JsonResult ArhiveOrder(int Id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (OrdersService.Instance.ArhiveOrder(Id))
            {
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }
            

            return result;

        }

        [HttpPost]
        public JsonResult RecoverOrder(int Id)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (OrdersService.Instance.RecoverOrder(Id))
            {
                result.Data = new { Success = true };
            }
            else
            {
                result.Data = new { Success = false };
            }


            return result;
        }

        public ActionResult OrderTable(string searchTerm, string status, int? pageNo)
        {
            OrdersViewModel model = new OrdersViewModel();
            model.Status = status;
            model.SearchTerm = searchTerm;
            model.InProgressCount = OrdersService.Instance.GetOrderCount("U procesu", false);
            model.DeliveredCount = OrdersService.Instance.GetOrderCount("Isporučena",false);
            model.UnresolvedCount = OrdersService.Instance.GetOrderCount("Nerešena",false);

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.Orders = OrdersService.Instance.SearchOrders(searchTerm,status, pageNo.Value, 5, false);

            var totalRecords = OrdersService.Instance.SearchOrdersCount(searchTerm,status);

            if (model.Orders != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, 5);

                return PartialView("OrderTable", model);
            }
            else
            {
                return HttpNotFound();
            }
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