using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decathlon.Models;
using System.IO;

namespace Decathlon.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        DataContext db = new DataContext();
        // GET: Slider
        public ActionResult Index()
        {
            var campaing = db.Slider.Where(x =>x.Delete == false).ToList();
            return View(campaing);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Slider slider, HttpPostedFileBase Image)
        {
            string ImagePath = "";
            string ImageName = "";

            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    ImageName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Images/slider"), ImageName);
                    Image.SaveAs(ImagePath);
                }

                Slider newsldier = new Slider
                {
                    Title = slider.Title,
                    Description = slider.Description,
                    Image = ImageName,
                    Link = slider.Link,
                    Status = slider.Status
                };
                db.Slider.Add(newsldier);
                db.SaveChanges();

                ViewBag.mesaj = "Yeni Slider Ekleme Başarılı";
            }
            catch
            {
                ViewBag.mesaj = "Beklenmeyen bir hata oluştu lütfen tekrar deneyiniz";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var sldier = db.Slider.Find(id);
            return View(sldier);
        }
        [HttpPost]
        public ActionResult Edit(Slider slider, HttpPostedFileBase Image)
        {
            var editsldier = db.Slider.Find(slider.Id);
            string ImagePath = "";
            string ImageName = "";

            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    ImageName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Images/sldier"), ImageName);
                    Image.SaveAs(ImagePath);
                    editsldier.Image = ImageName;
                }

                editsldier.Title = slider.Title;
                editsldier.Description = slider.Description;
                editsldier.Link = slider.Link;
                editsldier.Status = slider.Status;
                db.SaveChanges();

                ViewBag.mesaj = "Sldier Düzenleme Başarılı";
                return Redirect("~/Slider/Edit?id="+editsldier.Id);
            }
            catch
            {
                ViewBag.mesaj = "Beklenmeyen bir hata oluştu lütfen tekrar deneyiniz";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var slider = db.Slider.Find(id);
            slider.Delete = true;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}