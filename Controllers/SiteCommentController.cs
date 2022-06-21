using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decathlon.Models;
using Decathlon.AdminModel;
namespace Decathlon.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SiteCommentController : Controller
    {
        DataContext db = new DataContext();
        // GET: SiteComment
        public ActionResult Index()
        {
            SiteCommentModel model = new SiteCommentModel() {
                SiteComment = db.SiteComment.Where(x => x.Delete == false).ToList(),
                User = db.User.ToList()
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var detail = db.SiteComment.Find(id);
            SiteCommentModel model = new SiteCommentModel()
            {
                SingleSiteComment = detail,
                User = db.User.ToList()
            };
           
            if (detail.View == false)
            {
                detail.View = true;
                db.SaveChanges();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Detail(SiteComment siteComment)
        {
            var detail = db.SiteComment.Find(siteComment.Id);
            detail.Status = siteComment.Status;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sitecomment = db.SiteComment.Find(id);
            sitecomment.Delete = true;
            sitecomment.Status = false;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}