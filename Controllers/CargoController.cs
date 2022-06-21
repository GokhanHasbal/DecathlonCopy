using Decathlon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Decathlon.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CargoController : Controller
    {
        DataContext db = new DataContext();
        // GET: Cargo
        public ActionResult Index()
        {
            var cargo = db.Cargo.Where(x => x.Status == true && x.Delete == false).ToList();
            return View(cargo);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Cargo cargo, HttpPostedFileBase Logo)
        {
            string ImagePath = "";
            string ImageName = "";

            try
            {
                if (Logo != null && Logo.ContentLength > 0)
                {
                    ImageName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Logo.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Images/cargo"), ImageName);
                    Logo.SaveAs(ImagePath);
                }

                Cargo newcargo = new Cargo
                {
                    Name = cargo.Name,
                    Description = cargo.Description,
                    Logo = ImageName,
                    Status = cargo.Status
                };
                db.Cargo.Add(newcargo);
                db.SaveChanges();

                ViewBag.mesaj = "Yeni Kargo Ekleme Başarılı";
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
            var cargo = db.Cargo.Find(id);
            return View(cargo);
        }
        [HttpPost]
        public ActionResult Edit(Cargo cargo, HttpPostedFileBase Logo, HttpPostedFileBase Image)
        {
            var editcargo = db.Cargo.Find(cargo.Id);
            string ImagePath = "";
            string ImageName = "";
            string LogoName = "";

            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    ImageName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Images/cargo"), ImageName);
                    Image.SaveAs(ImagePath);
                    editcargo.Image = ImageName;
                }
                if (Logo != null && Logo.ContentLength > 0)
                {
                    LogoName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Logo.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Images/cargo"), LogoName);
                    Logo.SaveAs(ImagePath);
                    editcargo.Logo = LogoName;
                }
                editcargo.Name = cargo.Name;
                editcargo.Description = cargo.Description;
                editcargo.Status = cargo.Status;
                db.SaveChanges();

                ViewBag.mesaj = "Kargo Düzenleme Başarılı";
                return Redirect("~/Cargo/Edit?id=" + cargo.Id);
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
            var cargo = db.Cargo.Find(id);
            cargo.Delete = true;
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}