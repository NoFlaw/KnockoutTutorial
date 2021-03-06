﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Data;
using Web.Models;

namespace Web.Controllers
{
    public class BlogsKnockoutController : Controller
    {
        private readonly DatabaseContext _db = new DatabaseContext();

        // GET: BlogKnockout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllBlogs()
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
                return Json(new { success =false,  message = "WHOA! That was unexpected?!?" }); 

            foreach (var blogViewModel in blogs)
            {
                if (!string.IsNullOrEmpty(Convert.ToString(blogViewModel.BlogId)) && blogViewModel.BlogId > 0)
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

            return Json(new { success = true, message = string.Format("Sucessfully processed {0} item(s).", blogs.Count)});
        }

        
        public ActionResult RemoveBlog(int id)
        {
            if (id <= 0 || string.IsNullOrEmpty(Convert.ToString(id)))
                return Json(new { success = false, message = "WHOA! That was unexpected?!?" }); 

            var blog = _db.Blogs.Find(id);
            _db.Blogs.Remove(blog);
            _db.Blogs.Remove(blog);
            _db.SaveChanges();
            return Json(new { success = true, message = "Removed blog!"}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RemoveAllBlogs(List<BlogViewModel> blogs)
        {
            if (!blogs.Any())
                return Json(new {success = true, message = "No blogs to DELETE..."}, JsonRequestBehavior.AllowGet);
            
            foreach (var blogFound in blogs.Select(blogViewModel => 
                _db.Blogs.Find(blogViewModel.BlogId)).Where(blogFound => blogFound != null))
            {
                _db.Blogs.Remove(blogFound);
            }

            _db.SaveChanges();

            return Json(new { success = true, message = "Removed all blogs!" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveBlog(BlogViewModel blog)
        {
            Blog realBlog;
            realBlog = _db.Blogs.Find(blog.BlogId);

            if (realBlog == null)
            {
                realBlog = new Blog()
                {
                    Author = blog.Author,
                    BlogDescription = blog.Description,
                    BlogId = blog.BlogId,
                    DateCreated = blog.DateCreated,
                    Title = blog.Title
                };
                _db.Blogs.Add(realBlog);
            }
            else
            {
                realBlog.Author = blog.Author;
                realBlog.BlogDescription = blog.Description;
                realBlog.BlogId = blog.BlogId;
                realBlog.DateCreated = blog.DateCreated;
                realBlog.Title = blog.Title;
            }

            _db.SaveChanges();

            return Json(new { success = true, message = "Blog Saved" }, JsonRequestBehavior.AllowGet);
        }
    }
}