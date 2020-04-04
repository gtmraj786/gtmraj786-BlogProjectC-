using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data.Entity;

namespace NewBlogProject.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext() : base("BlogConnection")
        { }

        public DbSet<Author> AuthorsTable { get; set; }
        public DbSet<Category> CategoriesTable { get; set; }

        public DbSet<Post> PostsTable { get; set; }

    }
}