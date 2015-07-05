using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required(ErrorMessage = "Date Created is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        [DisplayName("Date Created")]
        
        public DateTime DateCreated { get; set; }

        [MaxLength(10, ErrorMessage = "(10) Character Maximum, (5) Character Minimum"), MinLength(1)]
        public string Author { get; set; }

        [Required]
        [DisplayName("Blog")]
        public string BlogDescription { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
