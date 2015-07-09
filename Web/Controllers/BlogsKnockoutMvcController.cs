using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Data;
using PerpetuumSoft.Knockout;
using Web.Models.koMvc;

namespace Web.Controllers
{
    public class BlogsKnockoutMvcController : KnockoutController
    {
        private readonly DatabaseContext _db = new DatabaseContext();

        // GET: BlogsKnockoutMvc
        public ActionResult Index()
        {
            var model = new BlogsEditorModel {Blogs = new List<BlogsEditorBlogModel>()};

            var blogs = _db.Blogs.ToList();

            model.Blogs = blogs.Select(blog => new BlogsEditorBlogModel
            {
                BlogId = blog.BlogId,
                Author = blog.Author,
                DateCreated = blog.DateCreated,
                Description = blog.BlogDescription,
                Title = blog.Title

            }).ToList();

            return View(model);
        }

        public ActionResult AddBlog(BlogsEditorModel model)
        {
            model.AddBlog();
            return Json(model);
        }

        public ActionResult DeleteBlog(BlogsEditorModel model, int blogIndex)
        {
            model.DeleteBlog(blogIndex);
            return Json(model);
        }

        //public ActionResult AddPost(BlogsEditorModel model, int blogIndex)
        //{
        //    model.AddPost(blogIndex);
        //    return Json(model);
        //}

        //public ActionResult DeletePhone(BlogsEditorModel model, int blogIndex, int postIndex)
        //{
        //    model.DeletePost(blogIndex, postIndex);
        //    return Json(model);
        //}

        public ActionResult SaveJson(BlogsEditorModel model)
        {
            model.SaveJson();
            return Json(model);
        }
    }
}