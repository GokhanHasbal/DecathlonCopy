using Decathlon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Decathlon.Controllers
{
    [Authorize(Roles ="Admin")]
    public class SitePageController : Controller
    {
        DataContext db = new DataContext();
        // GET: SitePage
        public ActionResult Index()
        {
            var page = db.Page.Where(x => x.Delete == false).ToList();
            return View(page);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Page page)
        {
            return View();
        }
        public ActionResult Edit(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Edit(Page page)
        {
            return View();
        }
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}