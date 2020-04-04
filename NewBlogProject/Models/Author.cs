using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NewBlogProject.Models
{
    [Table("tblAuthors")]
    public class Author
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Aid { get; set; }

        [StringLength(15, MinimumLength = 4, ErrorMessage = "User Name 4 to 15 characters")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}