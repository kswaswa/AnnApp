﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AnnApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AnnApp.Controllers
{
    public class AnnsController : Controller
    {
        // A reference to our database so we can access our tables and information.
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Anns
        public ActionResult Index()
        {
            // We want to save this and pass to html every time this page loads.
            if (isProfessorUser())
            {
                ViewBag.Name = "Professor";
            }
            else
            {
                ViewBag.Name = "Student";
            }

            return View();
        }

        /**
         * Returns the total list of Anns for the User we are on.
         * (none for null/no user)
         * */
        private IEnumerable<Ann> GetMyAnns()
        {
            // Get current user ID using the Identity framework
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);

            ApplicationUser newUser = new ApplicationUser();

            if (currentUser != null)
            {
                // only doing (if not null) so every user w account can see all announcements
                return db.Anns.ToList().Where(x => x.User != null);
            }
            else
            {
                // empty list
                return db.Anns.ToList().Where(x => x.User == newUser);
            }
        }
        
        /**
         * Simply sends the list of anns to the partial view _AnnTable.
         * */
        public ActionResult BuildAnnTable()
        {
            if (isProfessorUser())
            {
                ViewBag.Name = "Professor";
            }
            else
            {
                ViewBag.Name = "Student";
            }

            return PartialView("_AnnTable", GetMyAnns());
        }

        /**
         * Returns the list of Anns in order of what is checked on the page
         * to the partial view.
         * */
        public ActionResult SortAnnTable(string check)
        {
            var myAnns = GetMyAnns();
            var myRet = myAnns;

            // If title is checked, order by title, etc.
            if (check == "Title")
            {
                myRet = myAnns.OrderBy(x => x.Title);
            }

            if (check == "Content")
            {
                myRet = myAnns.OrderBy(x => x.Content);
            }

            if (check == "PostDate")
            {
                myRet = myAnns.OrderBy(x => x.PostDate);
            }

            return PartialView("_AnnTable", myRet);
        }

        /**
         * Returns list of Anns with string entered into filter title box
         * to partial view.
         * */
        public ActionResult FilterTitle(string title)
        {
            if (isProfessorUser())
            {
                ViewBag.Name = "Professor";
            }
            else
            {
                ViewBag.Name = "Student";
            }

            var myAnns = GetMyAnns();
            // Only keep those titles that contain the string they entered.
            var myTitle = myAnns.Where(x => x.Title.Contains(title));

            return PartialView("_AnnTable", myTitle);
        }

        /**
         * Returns list of Anns with string entered into filter content box
         * to partial view.
         * */
        public ActionResult FilterContent(string content)
        {
            if (isProfessorUser())
            {
                ViewBag.Name = "Professor";
            }
            else
            {
                ViewBag.Name = "Student";
            }

            var myAnns = GetMyAnns();
            var myContent = myAnns.Where(x => x.Content.Contains(content));

            return PartialView("_AnnTable", myContent);
        }

        /**
         * Returns list of Anns with string entered into filter post date box
         * to partial view.
         * */
        public ActionResult FilterPostDate(DateTime postDate)
        {
            if (isProfessorUser())
            {
                ViewBag.Name = "Professor";
            }
            else
            {
                ViewBag.Name = "Student";
            }

            var myAnns = GetMyAnns();
            var myPostDate = myAnns.Where(x => x.PostDate.Equals(postDate));

            return PartialView("_AnnTable", myPostDate);
        }

        /**
         * Returns the comments for the ann you are on to the partial view
         * that renders the comment table.
         * */
        public ActionResult BuildCommentTable()
        {
            // Gets ID from details function
            var myId = int.Parse(Session["ID"].ToString());

            var myAnns = GetMyAnns();
            var myAnn = GetMyAnn(myAnns, myId);

            // Gets comments from database where their ann is the current ann
            var myC1 = db.Comments.Where(x => x.Ann.ID == myId);

            return PartialView("_CommentsTable", myC1);
        }

        /**
         * Gets all of the users and subtracts the vieweds' users
         * who have viewed the current ann to return the list of who
         * has not viewed ann to the partial view that renders this list.
         * */
        public ActionResult BuildViewedAnnTable()
        {
            var myAnns = GetMyAnns();
            var myId = int.Parse(Session["ID"].ToString());
            var myAnn = GetMyAnn(myAnns, myId);

            // Get vieweds for current ann
            var myV1 = db.Vieweds.Where(x => x.Ann.ID == myId);

            var currentUser = new ApplicationUser();
            if (User.Identity.IsAuthenticated)
            {
                string currentUserId = User.Identity.GetUserId();
                currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
            }

            var nonViewers = new List<ApplicationUser>();
            // Get all users in user database
            var allUsers = db.Users.ToList();
            foreach (var user in allUsers)
            {
                // If user in all user is not in viewed list for this Ann, add to new list
                if (myV1.Where(x => x.User.Id == user.Id).Count() < 1)
                {
                    // This subtracts users that have viewed ann from all user list
                    nonViewers.Add(user);
                }
            }
            
            return PartialView("_ViewedAnn", nonViewers);
        }

        /**
         * Adds new comment to database and returns comments
         * of current ann to partial view that renders comment list.
         * */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(string comment, int id)
        {
            var anns = GetMyAnns();

            int myId = id;

            var myAnn = GetMyAnn(anns, myId);
            if (myAnn == null)
            {
                return HttpNotFound();
            }

            try
            {
                // Add comment to comment database
                var myComment = new Comment();
                myComment.comment = comment;
                myComment.Ann = myAnn;
                db.Comments.Add(myComment);
                db.SaveChanges();
            }
            catch
            {
                return HttpNotFound();
            }

            // Get all comments for current ann
            var retComments = db.Comments.Where(m => m.Ann.ID == myId);

            return PartialView("_CommentsTable", retComments);
        }

        /**
         * Return the ann for the id given.
         * */
        private Ann GetMyAnn(IEnumerable<Ann> anns, int id)
        {
            return db.Anns.Find(id);
        }

        // GET: Anns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ann ann = db.Anns.Find(id);
            if (ann == null)
            {
                return HttpNotFound();
            }

            if (isProfessorUser())
            {
                ViewBag.Name = "Professor";
            }
            else
            {
                ViewBag.Name = "Student";
            }

            Session["ID"] = id;

            var currentUser = new ApplicationUser();
            if (User.Identity.IsAuthenticated)
            {
                string currentUserId = User.Identity.GetUserId();
                currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
            }

            Viewed myViewed = new Viewed();
            myViewed.Ann = ann;
            myViewed.User = currentUser;

            // Only adds the username once.
            var viewedsId = db.Vieweds.Where(x => x.Ann.ID == ann.ID);
            if (viewedsId == null)
            {
                db.Vieweds.Add(myViewed);
            }
            db.SaveChanges();

            return View(ann);
        }

        /**
         * Checks if the current user role is professor or not (student)
         * */
        public Boolean isProfessorUser()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());

                try
                {
                    if (s[0] == "Professor")
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }

            return false;
        }

        // GET: Anns/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Anns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Content,PostDate")] Ann ann)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);
                ann.User = currentUser;

                if (isProfessorUser())
                {
                    ViewBag.Name = "Professor";
                }
                else
                {
                    ViewBag.Name = "Student";
                }
                
                db.Anns.Add(ann);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ann);
        }

        // POST: Anns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCreate([Bind(Include = "ID,Title,Content,PostDate")] Ann ann)
        {
                //if (ModelState.IsValid)
                try
                {
                    string currentUserId = User.Identity.GetUserId();
                    ApplicationUser currentUser = db.Users.FirstOrDefault
                        (x => x.Id == currentUserId);
                    ann.User = currentUser;

                    if (isProfessorUser())
                    {
                        ViewBag.Name = "Professor";
                    }
                    else
                    {
                        ViewBag.Name = "Student";
                    }

                    db.Anns.Add(ann);
                    db.SaveChanges();
                }
                catch
            {
                return PartialView("_AnnTableFail", GetMyAnns());
            }

            return PartialView("_AnnTable", GetMyAnns());
        }

        // GET: Anns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ann ann = db.Anns.Find(id);

            if (ann == null)
            {
                return HttpNotFound();
            }

            return View(ann);
        }

        // POST: Anns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,PostDate")] Ann ann)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ann).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ann);
        }

        // GET: Anns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Ann ann = db.Anns.Find(id);

            if (ann == null)
            {
                return HttpNotFound();
            }

            return View(ann);
        }

        // POST: Anns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ann ann = db.Anns.Find(id);
            db.Anns.Remove(ann);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
