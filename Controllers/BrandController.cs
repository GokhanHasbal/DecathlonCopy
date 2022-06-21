using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decathlon.Models;
using System.IO;
namespace Decathlon.Controllers
{
    [Authorize]
    public class BrandController : Controller
    {
        DataContext db = new DataContext();
        // GET: Brand
        public ActionResult Index()
        {
            var model = db.Brand.Where(x => x.Delete == false).ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Brand brand)
        {
            db.Brand.Add(brand);
            db.SaveChanges();
            ViewBag.mesaj = "Marka Ekleme Başarılı";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var brand = db.Brand.Find(id);
            if (brand != null)
            {
                return View(brand);
            }
            else
            {
                return RedirectToAction("index");
            }
        }
        [HttpPost]
        public ActionResult Edit(Brand brand,HttpPostedFileBase Image)
        {
            var editbrand = db.Brand.Find(brand.Id);
            string imagePath = "";
            string imageName = "";
            try
            {
                if(Image!=null && Image.ContentLength > 0)
                {
                    imageName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    imagePath = Path.Combine(Server.MapPath("~/Content/images/brand"), imageName);
                    Image.SaveAs(imagePath);
                    editbrand.Image = imageName;
                }
            }
            catch {
                ViewBag.mesaj = "Resim eklerken beklenmedik bir hata oluştu";
            }
            editbrand.Name = brand.Name;
            editbrand.Description = brand.Description;
            editbrand.Status = brand.Status;
            db.SaveChanges();
            ViewBag.mesaj = "Marka Düzenleme Başarılı";
            return View(editbrand);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var brand = db.Brand.Find(id);
            brand.Delete = true;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}