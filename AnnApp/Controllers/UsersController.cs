using AnnApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// help - http://social.technet.microsoft.com/wiki/contents/articles/33229.asp-net-mvc-5-security-and-creating-user-role.aspx

namespace AnnApp.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            var user = User.Identity;
            ViewBag.Name = user.Name;

            ViewBag.displayMenu = "No";

            if (isProfessorUser())
            {
                ViewBag.displayMenu = "Yes";
            }

            return View();
        }

        public Boolean isProfessorUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if(s[0].ToString() == "Professor")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}