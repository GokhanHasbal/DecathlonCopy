using Decathlon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Decathlon.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CampaingController : Controller
    {
        DataContext db = new DataContext();
        // GET: Cargo
        public ActionResult Index()
        {
            var campaing = db.Campaing.Where(x => x.Delete == false).ToList();
            return View(campaing);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Campaing campaing, HttpPostedFileBase Image)
        {
            string ImagePath = "";
            string ImageName = "";

            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    ImageName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Images/campaing"), ImageName);
                    Image.SaveAs(ImagePath);
                }

                Campaing newcampaing = new Campaing
                {
                    Title = campaing.Title,
                    Description = campaing.Description,
                    Image = ImageName,
                    Link = campaing.Link,
                    Discount = campaing.Discount,
                    StartDate = campaing.StartDate,
                    StopDate = campaing.StopDate,
                    Status = campaing.Status
                };
                db.Campaing.Add(newcampaing);
                db.SaveChanges();

                ViewBag.mesaj = "Yeni Kampanaya Ekleme Başarılı";
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
            var campaing = db.Campaing.Find(id);
            return View(campaing);
        }
        [HttpPost]
        public ActionResult Edit(Campaing campaing, HttpPostedFileBase Image)
        {
            var editcampaing = db.Campaing.Find(campaing.Id);
            string ImagePath = "";
            string ImageName = "";

            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    ImageName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Images/campaing"), ImageName);
                    Image.SaveAs(ImagePath);
                    editcampaing.Image = ImageName;
                }

                editcampaing.Title = campaing.Title;
                editcampaing.Description = campaing.Description;
                editcampaing.Discount = campaing.Discount;
                editcampaing.StartDate = campaing.StartDate;
                editcampaing.StopDate = campaing.StopDate;
                editcampaing.Link = campaing.Link;
                editcampaing.Status = campaing.Status;
                db.SaveChanges();

                ViewBag.mesaj = "Kampanya Düzenleme Başarılı";
                return Redirect("~/Campaing/Edit?id=" + campaing.Id);
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
            var campaing = db.Campaing.Find(id);
            campaing.Delete = true;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}