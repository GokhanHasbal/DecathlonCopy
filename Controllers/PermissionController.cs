using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decathlon.Models;

namespace Decathlon.Controllers
{
    [Authorize]
    public class PermissionController : Controller
    {
        DataContext db = new DataContext();
        // GET: Permission
        public ActionResult Index()
        {
            var permission = db.Permission.Where(x => x.Delete == false).ToList();
            return View(permission);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Permission permission)
        {
            db.Permission.Add(permission);
            db.SaveChanges();
            ViewBag.mesaj = "Yetki Ekleme Başarılı";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var permission = db.Permission.Find(id);
            return View(permission);
        }
        [HttpPost]
        public ActionResult Edit(Permission permission)
        {
            var editpermission = db.Permission.Find(permission.Id);

            editpermission.Name = permission.Name;
            editpermission.Status = permission.Status;
            db.SaveChanges();
            ViewBag.mesaj = "Yetki Düzenleme Başarılı";
            return Redirect("~/Permission/Edit?id=" + permission.Id);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var permission = db.Permission.Find(id);
            permission.Delete = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}