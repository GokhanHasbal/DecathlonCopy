using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decathlon.Models;
namespace Decathlon.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsGetController : Controller
    {
        DataContext db = new DataContext();
        // GET: SiteComment
        public ActionResult Index()
        {
            var news = db.NewsGet.Where(x => x.Delete == false).ToList();
            return View(news);
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var detail = db.NewsGet.Find(id);


            if (detail.View == false)
            {
                detail.View = true;
                db.SaveChanges();
            }
            return View(detail);
        }
        [HttpPost]
        public ActionResult Detail(NewsGet newsGet)
        {
            var detail = db.NewsGet.Find(newsGet.Id);
            detail.Status = newsGet.Status;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var newsGet = db.NewsGet.Find(id);
            newsGet.Delete = true;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
