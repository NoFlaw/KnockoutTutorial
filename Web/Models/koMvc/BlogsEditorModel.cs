using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Web.Models.koMvc
{
    public class BlogsEditorModel
    {
        public List<BlogsEditorBlogModel> Blogs { get; set; }
        public string LastSavedJson { get; set; }

        public void AddBlog()
        {
            Blogs.Add(new BlogsEditorBlogModel());
        }

        public void DeleteBlog(int blogIndex)
        {
            if (blogIndex >= 0 && blogIndex < Blogs.Count)
                Blogs.RemoveAt(blogIndex);
        }

        //public void AddPost(int blogIndex)
        //{
        //    if (blogIndex >= 0 && blogIndex < Blogs.Count)
        //        Blogs[blogIndex].AddPhone();
        //}

        //public void DeletePost(int blogIndex, int postIndex)
        //{
        //    if (blogIndex >= 0 && blogIndex < Blogs.Count)
        //        Blogs[blogIndex].DeletePhone(postIndex);
        //}

        public void SaveJson()
        {
            LastSavedJson = new JavaScriptSerializer().Serialize(this);
        }
    }
}
    