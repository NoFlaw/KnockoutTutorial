using System;
using System.Data.Entity;
using System.Collections.Generic;

namespace Data
{
    public class Seeder : DropCreateDatabaseAlways<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            var blogs = new List<Blog>
            {
                new Blog
                {
                    BlogDescription = "Welcome to my cool blog, here it is, thanks for coming, hope to see you again soon!",
                    Author = "Admin",
                    Title = "1st Blog Title",
                    DateCreated = Convert.ToDateTime("06/05/2013")
                },
                new Blog
                {
                    BlogDescription = "This blog is all about COMPUTERS!!",
                    Author = "Jack",
                    Title = "2nd Blog Title",
                    DateCreated = Convert.ToDateTime("06/06/2013")
                },
                new Blog
                {
                    BlogDescription = "Another cool blog for you to read, you should check it out!",
                    Author = "Jill",
                    Title = "3rd Blog Title",
                    DateCreated = Convert.ToDateTime("06/07/2013")
                },
                new Blog
                {
                    BlogDescription = "RANDOM random RanDoM rAnDoM",
                    Author = "Random User",
                    Title = "Random Stuff",
                    DateCreated = DateTime.Now
                }
            };

            var posts = new List<Post>
            {
                new Post
                {
                    Blog = blogs[0],
                    DateCreated = Convert.ToDateTime("07/31/1984"),
                    CurrentPost = "This blog sux... so bad!",
                    NickName = "koolkid"
                },
                new Post
                {
                    Blog = blogs[1],
                    DateCreated = Convert.ToDateTime("08/07/1986"),
                    CurrentPost = "This blog is ok.",
                    NickName = "Pleirosei"
                },

                new Post
                {
                    Blog = blogs[2],
                    DateCreated = Convert.ToDateTime("11/05/2013"),
                    CurrentPost = "THIS BLOG IS AWESOME!#@^!!",
                    NickName = "eXcLuSiVe"
                },
                new Post
                {
                    Blog = blogs[2],
                    DateCreated = Convert.ToDateTime("01/01/2012"),
                    CurrentPost = "Really good blog! READ IT!",
                    NickName = "raZ0r"
                }

            };

            blogs.ForEach(x => context.Blogs.Add(x));
            posts.ForEach(x => context.Posts.Add(x));

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
