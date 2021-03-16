using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace ASonline.Controllers
{
    public class SharedController : Controller
    {
        public JsonResult UploadImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            var list = new List<String>();
            try
            {

                for(var i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];

                    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                    var path = Path.Combine(Server.MapPath("~/content/images/productImage"), fileName);

                    file.SaveAs(path);

                    list.Add(string.Format("/content/images/productImage/{0}", fileName));                   
                   
                }
                result.Data = new { Success = true, ImageUrl = list };
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }
            return result;
        }
    }
}