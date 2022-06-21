using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decathlon.Models;
namespace Decathlon.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactMailController : Controller
    {
        DataContext db = new DataContext();
        // GET: SiteComment
        public ActionResult Index()
        {
            var cm = db.ContactMail.Where(x => x.Delete == false).ToList();
            return View(cm);
        }
        [HttpGet]
        public ActionResult Detail(int id)
        {
            var detail = db.ContactMail.Find(id);
            
            

            if (detail.View == false)
            {
                detail.View = true;
                db.SaveChanges();
            }
            return View(detail);
        }
        [HttpPost]
        public ActionResult Detail(ContactMail contact)
        {
            var detail = db.ContactMail.Find(contact.Id);
            detail.Status = contact.Status;
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var contact = db.ContactMail.Find(id);
            contact.Delete = true;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}