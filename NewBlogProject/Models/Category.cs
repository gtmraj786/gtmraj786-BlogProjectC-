using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NewBlogProject.Models
{

    [Table("tblCategories")]
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Cid { get; set; }

        public string CName { get; set; }



        public DateTime? StartDate { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

    }
}