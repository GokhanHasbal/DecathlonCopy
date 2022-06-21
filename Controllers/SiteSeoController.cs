using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decathlon.Models;
namespace Decathlon.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SiteSeoController : Controller
    {
        DataContext db = new DataContext();
        // GET: SiteComment
        public ActionResult Index()
        {
            var seo = db.Seo.Find(2);
            return View(seo);
        }
       
        [HttpPost]
        public ActionResult Index(Seo seo, HttpPostedFileBase Logo, HttpPostedFileBase Ico)
        {
            var detail = db.Seo.Find(seo.Id);

            string ImagePath = "";
            string IcoName = "";
            string LogoName = "";
            try
            {
                if (Logo != null && Logo.ContentLength > 0)
                {
                    LogoName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Logo.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Images/seo"), LogoName);
                    Logo.SaveAs(ImagePath);
                    detail.Logo = LogoName;
                }
                if (Ico != null && Ico.ContentLength > 0)
                {
                    IcoName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Ico.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Images/seo"), IcoName);
                    Ico.SaveAs(ImagePath);
                    detail.Ico = IcoName;
                }
                detail.Description = seo.Description;
                detail.Title = seo.Title;
                detail.Keywords = seo.Keywords;
                detail.GoogleAnalityc = seo.GoogleAnalityc;
                detail.YandexMetrica = seo.YandexMetrica;
                detail.Logo = seo.Logo;
                detail.Ico = seo.Ico;
                detail.Status = seo.Status;
                db.SaveChanges();
                return View(detail);
            }
            catch
            {
                ViewBag.mesaj = "Beklenmeyen bir hata oluştu lütfen tekrar deneyiniz";
            }
            return View();

            
        }
        
    }
}