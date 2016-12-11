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