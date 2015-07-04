using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Data;

namespace Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly Entities _db = new Entities();

        // GET: Blogs
        public ActionResult Index()
        {
            return View(_db.Blogs.ToList());
        }

        // GET: Blogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var blog = _db.Blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: Blogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogId,Title,DateCreated,Author,BlogDescription")] Blog blog)
        {
            if (!ModelState.IsValid) return View(blog);
            blog.DateCreated = DateTime.Now;
            _db.Blogs.Add(blog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Blogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var blog = _db.Blogs.Find(id);
            
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BlogId,Title,DateCreated,Author,BlogDescription")] Blog blog)
        {
            if (!ModelState.IsValid) 
                return View(blog);
            
            _db.Entry(blog).State = EntityState.Modified;
            _db.SaveChanges();
            
            return RedirectToAction("Index");
        }

        // GET: Blogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var blog = _db.Blogs.Find(id);
            
            if (blog == null)
            {
                return HttpNotFound();
            }
            
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var blog = _db.Blogs.Find(id);
            _db.Blogs.Remove(blog);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        //GET: Blog/CreatePost?blogId={0}
        public ActionResult CreatePost(int blogId)
        {
            //TODO: Add viewmodel code here due to displaying data from Posts & Blog

            var blog = _db.Blogs.Find(blogId);

            if (blog == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.BlogDesc = blog.BlogDescription;

            return View();
        }

        //
        // POST: /Blog/Post/Create{post, blogId}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePost(Post post, int blogId)
        {
            if (!ModelState.IsValid) return View();
 
            var currentBlog = _db.Blogs.Find(blogId);
                
            post.BlogId = currentBlog.BlogId;
            post.DateCreated = DateTime.Now;

            _db.Posts.Add(post);
                
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            
            base.Dispose(disposing);
        }
    }
}
