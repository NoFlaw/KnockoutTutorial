using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required]
        public string CurrentPost { get; set; }

        [Required]
        public string NickName { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }


        [ForeignKey("BlogId")]
        public virtual Blog Blog { get; set; }

        public int BlogId { get; set; }
    } 
}
