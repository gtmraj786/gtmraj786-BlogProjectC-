using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewBlogProject.Models;
using System.Web.Security;

namespace NewBlogProject.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account

        public BlogContext db = new BlogContext();
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ActionName("SignUp")]
        public ActionResult PostSignUp()
        {
            if (ModelState.IsValid)
            {
                Author author = new Author();

                TryUpdateModel(author);
                db.AuthorsTable.Add(author);
                db.SaveChanges();
            }

            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Login")]
        public ActionResult PostLogin(NewBlogProject.Models.MemberShip membership)
        {
            bool isValid = db.AuthorsTable.Any(x => x.Name == membership.asd && x.Password == membership.Password);
            if (isValid)
            {

                FormsAuthentication.SetAuthCookie(membership.asd, false);
                return RedirectToAction("Home", "Blog");
            }
            ModelState.AddModelError("", "Invalid user name and password");
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}