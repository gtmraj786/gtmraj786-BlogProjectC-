using System;
using System.Collections.Generic;
using System.Linq;

using System.Net;
using System.Web.Mvc;

using NewBlogProject.Models;
using System.Security.Claims;

using Microsoft.AspNet.Identity;
using System.Security.Principal;
using WebMatrix.WebData;

namespace NewBlogProject.Controllers
{
    public class BlogController : Controller
    {
        // GET: Blog

        BlogContext db = new BlogContext();
        [AllowAnonymous]
        public ActionResult Home()
        {
            List<Post> posts = db.PostsTable.ToList();

            ViewBag.password = "lnlknkn";//posts[0].Author.Password;
            return View(posts);
        }

        [AllowAnonymous]
        public ActionResult CreateCategory()
        {

            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ActionName("CreateCategory")]
        public ActionResult PostCreateCategory()
        {

            Category category = new Category();

            TryUpdateModel(category);
            category.StartDate = DateTime.Now;
            db.CategoriesTable.Add(category);
            db.SaveChanges();


            return RedirectToAction("Home");
        }


        public ActionResult CreatePost()
        {

            List<Category> categories = db.CategoriesTable.ToList();
            ViewBag.postType = categories;
            return View();
        }

        [HttpPost]
        [ActionName("CreatePost")]
        public  ActionResult PostCreatePost(FormCollection collection)
        {

            string postType = collection["postType"];
            string []CId = postType.Split(',');
            List<Category> categories = new List<Category>(); ;

            foreach (string i in CId)
            {
                Category category = db.CategoriesTable.Find(int.Parse(i));
                categories.Add(category);
            }


            if (ModelState.IsValid)
            {

                Post post = new Post();


                TryUpdateModel(post);

                Author author1 = null;
                using (var context = new BlogContext())
                {
                    author1 = (from s in context.AuthorsTable
                                   where s.Name == User.Identity.Name
                                   select s).FirstOrDefault<Author>();
                }
         
                Author author = db.AuthorsTable.Find(author1.Aid);
               
                post.Author = author;
                
                post.Published = DateTime.Now;
                post.UpdatedAt = DateTime.Now;
                // post.content = post.Author.Password;
                //post.content = postType;

                post.Categories = categories;
                db.PostsTable.Add(post);
                db.SaveChanges();
            }

            return RedirectToAction("Home");
        }

        public ActionResult Details(int? id)
        {
            Post post = db.PostsTable.Find(id);
            
            if (User.Identity.Name==post.Author.Name)
            {
                if (id == null)
                {

                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Post Id Required");
                }
             
                if (post == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Post not Found");
                }
                return View(post);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Access Dinied");
            }
        }

        public ActionResult Edit(int? id)
        {
            Post post = db.PostsTable.Find(id);
            List<Category> categories = db.CategoriesTable.ToList();
            ViewBag.postType = categories;

            if (User.Identity.Name == post.Author.Name)
            {
                if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Post Id Required");
            }
           
            if (post == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Post not Found");
            }
            return View(post);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Access Dinied");
            }

        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {

            Post post = db.PostsTable.Find(id);
            
            foreach(Category cat in  post.Categories.ToList())
            {
                  post.Categories.Remove(cat);

            }
            string postType = collection["postType"];
        
            if (User.Identity.Name == post.Author.Name)
            {
                UpdateModel(post);

            post.UpdatedAt = DateTime.Now;

                if (postType != null)
                {
                    string[] CId = postType.Split(',');
                    List<Category> categories = new List<Category>();

                    foreach (string i in CId)
                    {
                        Category category = db.CategoriesTable.Find(int.Parse(i));
                        categories.Add(category);
                    }
                    post.Categories = categories;
                }
                db.SaveChanges();
            return RedirectToAction("Home");
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Access Dinied");
            }

        }

        public ActionResult Delete(int? id)
        {
            Post post = db.PostsTable.Find(id);
            if (User.Identity.Name == post.Author.Name)
            {
                if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Product Id  Required");
            }
           

            if (post == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Product not Found");
            }
            return View(post);

        }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Access Dinied");
    }
}


        [HttpPost]
        public ActionResult Delete(int id)
        {
            Post post = db.PostsTable.Find(id);
           if(User.Identity.Name == post.Author.Name)
            {
                db.PostsTable.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Home");

             }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound, "Access Dinied");
            }

        }


        [AllowAnonymous]
        public ActionResult About()
        {

            return View();
        }
    }

}