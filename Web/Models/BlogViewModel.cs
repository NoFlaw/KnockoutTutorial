using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class BlogViewModel
    {
        public int BlogId { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime DateCreated { get; set; }
    }
}