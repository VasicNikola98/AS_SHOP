using AS.Database;
using ASonline.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ASonline.Service
{
    public class CategoryService
    {
        #region Singleton
        public static CategoryService Instance
        {
            get
            {
                if (instance == null) instance = new CategoryService();

                return instance;
            }
        }

        private static CategoryService instance { get; set; }

        private CategoryService()
        {

        }
        #endregion

       
        #region GET
        public List<Category> GetCategories()
        {
            using(var ctx = new ASDbContext())
            {
                var categories = ctx.Categories.ToList();
                return categories;
            }
        }
        public List<Category> GetCategories(string search, int pageNo, int pageSize)
        {
            using (var ctx = new ASDbContext())
            {

                if (!String.IsNullOrEmpty(search))
                {
                    return ctx.Categories.Where(p => p.Name != null && p.Name.ToLower()
                    .Contains(search.ToLower()))
                    .OrderBy(x => x.Id)
                    .Skip((pageNo - 1) * pageSize)
                    .Include(x => x.Products)
                    .Take(pageSize)
                    .ToList();

                }
                else
                {
                    return ctx.Categories.OrderBy(x => x.Id)
                        .Skip((pageNo - 1) * pageSize)
                        .Include(x => x.Products)
                        .Take(pageSize)
                        .ToList();
                }

            }
        }
        public List<Category> GetFeaturedCategories()
        {
            using (var ctx = new ASDbContext())
            {
                var categories = ctx.Categories.Where(x => x.IsFeatured && x.ImageUrl != null).ToList();
                return categories;
            }
        }
        public Category GetCategoryById(int id)
        {
           
                using(var ctx = new ASDbContext())
                {
                    var category = ctx.Categories.Find(id);
                    return category;
                }

        }
        public int GetCategoriesCount(string search)
        {
            using (var ctx = new ASDbContext())
            {
                if (!String.IsNullOrEmpty(search))
                {
                    return ctx.Categories.Where(p => p.Name != null && p.Name.ToLower()
                    .Contains(search.ToLower()))
                    .OrderBy(x => x.Id).Count();
                }
                else
                {
                    return ctx.Categories.Count();
                }

            }
        }
        #endregion

        #region Create
        public void SaveCategory(Category category)
        {
            using (var ctx = new ASDbContext())
            {
                if (category != null)
                {
                    ctx.Categories.Add(category);
                    ctx.SaveChanges();
                }
            }
        }
        #endregion

        #region Update
        public void UpdateCategory(Category category)
        {
            using(var ctx = new ASDbContext())
            {
                ctx.Entry(category).State = System.Data.Entity.EntityState.Modified;
                ctx.SaveChanges();
            }
        }
        #endregion

        #region Delete
        public void DeleteCategory(int Id)
      {
            using (var ctx = new ASDbContext())
            {
                var category = ctx.Categories.Where(x => x.Id == Id).Include(x => x.Products).FirstOrDefault();
                if (category != null)
                {
                    ctx.Products.RemoveRange(category.Products);
                    ctx.Categories.Remove(category);
                    ctx.SaveChanges();
                }
            }
        }
#endregion

    }
}
