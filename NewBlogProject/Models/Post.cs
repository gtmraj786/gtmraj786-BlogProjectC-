using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NewBlogProject.Models
{

    [Table("tblPosts")]
    public class Post
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Pid { get; set; }

        public string title { get; set; }
        public string content { get; set; }



        public DateTime? Published { get; set; }


        public DateTime? UpdatedAt { get; set; }

        public virtual Author Author { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}