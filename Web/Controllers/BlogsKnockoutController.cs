using System;
using System.Collections.Generic;
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
                BlogId = blog.BlogId,
                Author = blog.Author,
                DateCreated = blog.DateCreated,
                Description = blog.BlogDescription,
                Title = blog.Title
            }).ToList();

            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrUpdateBlogs(List<BlogViewModel> blogs)
        {
            if (!ModelState.IsValid)
                return Json(new { Response = "Error", Message = "ErrorMessage: WHOA! That was unexpected?!?" }); 

            foreach (var blogViewModel in blogs)
            {
                if (blogViewModel.BlogId > 0 && !string.IsNullOrEmpty(Convert.ToString(blogViewModel.BlogId)))
                {
                    var foundBlog = _db.Blogs.Find(blogViewModel.BlogId);

                    if (foundBlog == null) continue;
                    foundBlog.Author = blogViewModel.Author;
                    foundBlog.BlogDescription = blogViewModel.Description;
                    foundBlog.Title = blogViewModel.Title;
                }
                else
                {
                    _db.Blogs.Add(new Blog
                    {
                        Author = blogViewModel.Author,
                        BlogDescription = blogViewModel.Description,
                        Title = blogViewModel.Title,
                        DateCreated = DateTime.Now                      
                    });
                }
                
            }

            _db.SaveChanges(); 

            return Json(new { Response = "Success", Message = string.Format("Sucessfully processed {0} item(s).", blogs.Count)});
        }

        
        public JsonResult RemoveBlog(int id)
        {
            if (id <= 0 || string.IsNullOrEmpty(Convert.ToString(id)))
                return Json(new { Response = "Error", Message = "ErrorMessage: WHOA! That was unexpected?!?" }); 

            var blog = _db.Blogs.Find(id);
            _db.Blogs.Remove(blog);
            _db.SaveChanges();
            return Json(new { Response = "Success", Message = "Removed blog!"}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveAllBlogs(List<BlogViewModel> blogs)
        {
            if (!blogs.Any())
                return Json(new {Response = "Success", Message = "No blogs to DELETE..."}, JsonRequestBehavior.AllowGet);
            
            foreach (var blogFound in blogs.Select(blogViewModel => 
                _db.Blogs.Find(blogViewModel.BlogId)).Where(blogFound => blogFound != null))
            {
                _db.Blogs.Remove(blogFound);
            }

            _db.SaveChanges();

            return Json(new { Response = "Success", Message = "Removed all blogs!" }, JsonRequestBehavior.AllowGet);
        }
    }
}