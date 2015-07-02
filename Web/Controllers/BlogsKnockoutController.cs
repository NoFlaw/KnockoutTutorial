using System.Linq;
using System.Web.Mvc;
using Data;
using Web.Models;

namespace Web.Controllers
{
    public class BlogsKnockoutController : Controller
    {
        private readonly Entities _db = new Entities();

        // GET: BlogKnockout
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllBlogs()
        {
            var blogs = _db.Blogs.ToList();

            var viewModel = blogs.Select(blog => new BlogViewModel
            {
                Author = blog.Author, 
                DateCreated = blog.DateCreated, 
                Description = blog.BlogDescription, 
                Title = blog.Title

            }).ToList();

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
    }
}