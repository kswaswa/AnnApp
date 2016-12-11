using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnnApp.Controllers
{
    public class WordController : Controller
    {
        // GET: Word
        public ActionResult Index()
        {
            return View();
        }

        [WordDocument]
        public ActionResult DownloadViewedDocument()
        {
            ViewBag.AnnTitle = Session["title"];
            ViewBag.Viewed = Session["viewed"];
            ViewBag.WordDocumentFilename = "ViewedDocument";
            return View("Index");
        }
    }
}