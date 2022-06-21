using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decathlon.Models;
using Decathlon.AdminModel;
namespace Decathlon.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SupportController : Controller
    {
        // GET: Support
        DataContext db = new DataContext();
        // GET: SiteComment
        public ActionResult Index()
        {
            SupportModel model = new SupportModel()
            {
                Support = db.Support.Where(x => x.Delete == false).ToList(),
                User = db.User.ToList()
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var detail = db.Support.Find(id);
            SupportModel model = new SupportModel()
            {
                SingleSupport = detail,
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
        public ActionResult Detail(Support support)
        {
            var detail = db.Support.Find(support.Id);
            detail.Status = support.Status;
            detail.SupportStatus = support.SupportStatus;
            db.SaveChanges();
            //return Redirect("~/Support/Detail?id="+detail.Id);
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpPost]
        public ActionResult SupportStatus(Support support)
        {
            var detail = db.Support.Find(support.Id);
            detail.SupportStatus = support.SupportStatus;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var support = db.Support.Find(id);
            support.Delete = true;
            support.Status = false;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}