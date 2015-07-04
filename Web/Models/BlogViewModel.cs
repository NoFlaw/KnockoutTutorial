using System;

namespace Web.Models
{
    public class BlogViewModel
    {
        public int BlogId { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
    }
}