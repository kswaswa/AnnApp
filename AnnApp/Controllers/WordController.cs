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

        /**
         * Supposedly downloads a view as a word document (doesn't work).
         * */
        [WordDocument]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DownloadViewedDocument()
        {
            ViewBag.AnnTitle = Session["title"];
            ViewBag.Viewed = Session["viewed"];
            ViewBag.WordDocumentFilename = "ViewedDocument";
            return View("Index");
        }
    }
}