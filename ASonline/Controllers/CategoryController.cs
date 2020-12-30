using ASonline.Entities;
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
    public class CategoryController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult _CategoryTable(string search, int? pageNo)
        {
            CategorySearchViewModels model = new CategorySearchViewModels();


            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            var totalRecords = CategoryService.Instance.GetCategoriesCount(search);
            model.SearchTerm = search;

            model.Categories = CategoryService.Instance.GetCategories(search, pageNo.Value, 5);

            if (model.Categories != null)
            {
                model.Pager = new Pager(totalRecords, pageNo, 5);

                return PartialView("_CategoryTable", model);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult GetMainCategories()
        {
            var categories = CategoryService.Instance.GetCategories();
            return PartialView(categories);
        }

        #region Creation
        [HttpGet]
        public ActionResult Create()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            if (model != null)
            {
                Category category = new Category();
                category.Name = model.Name;
                category.Description = model.Description;
                category.ImageUrl = model.ImageUrl;
                category.IsFeatured = model.IsFeatured;

                CategoryService.Instance.SaveCategory(category);
            }

            return RedirectToAction("_CategoryTable");
        }
        #endregion

        #region Updation
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();

            var category = CategoryService.Instance.GetCategoryById(Id);

            if (category != null)
            {
                model.Id = category.Id;
                model.Name = category.Name;
                model.Description = category.Description;
                model.ImageUrl = category.ImageUrl;
                model.IsFeatured = category.IsFeatured;
                return PartialView(model);
            }
            else
            {
                return HttpNotFound();
            }

        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var existingCategory = CategoryService.Instance.GetCategoryById(model.Id);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.IsFeatured = model.IsFeatured;
            existingCategory.Products = ProductService.Instance.GetProductsByCategory(model.Id);

            if (!string.IsNullOrEmpty(model.ImageUrl))
            {
                existingCategory.ImageUrl = model.ImageUrl;
            }

            CategoryService.Instance.UpdateCategory(existingCategory);
           
            return RedirectToAction("_CategoryTable");
        }
        #endregion

        #region Deletion
        [HttpPost]
        public ActionResult Delete(int Id)
        {

            CategoryService.Instance.DeleteCategory(Id);

            return RedirectToAction("_CategoryTable");
        }
        #endregion

    }
}