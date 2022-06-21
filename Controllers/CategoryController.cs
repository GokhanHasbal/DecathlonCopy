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
    public class CategoryController : Controller
    {
        DataContext db = new DataContext();
        // GET: Category
        public ActionResult Index()
        {
            var category = db.Category.Where(x => x.Delete == false).ToList();
            return View(category);
        }
        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.category = db.Category.Where(x => x.MainCategoryId == 0).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Add(Category category)
        {
            Category newcategory = new Category();
            newcategory.Name = category.Name;
            newcategory.Description = category.Description;
            newcategory.MainCategoryId = category.MainCategoryId;
            newcategory.CompaignId = 0;

            newcategory.Status = category.Status;
            

            db.Category.Add(newcategory);
            db.SaveChanges();

            ViewBag.category = db.Category.Where(x => x.MainCategoryId == 0).ToList();

            ViewBag.mesaj = "Kategori Ekleme Başarılı";
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var category = db.Category.Find(id);
            if (category != null)
            {
                ViewBag.category = db.Category.Where(x => x.MainCategoryId == 0).ToList();
                ViewBag.campaign = db.Campaing.Where(x => x.Status==true).ToList();
                if (category.MainCategoryId != 0)
                {
                    ViewBag.main = db.Category.FirstOrDefault(x => x.Id == category.MainCategoryId).Name;
                }
                if (category.CompaignId != 0)
                {
                    ViewBag.compaignName = db.Campaing.FirstOrDefault(x => x.Id == category.CompaignId).Title;
                }
                return View(category);
            }
            else
            {
                return RedirectToAction("index");
            }
            
        }
        [HttpPost]
        public ActionResult Edit(Category category, HttpPostedFileBase Image)
        {
            var editcategory = db.Category.Find(category.Id);

            editcategory.Name = category.Name;
            editcategory.Description = category.Description;
            editcategory.Status = category.Status;
            editcategory.MainCategoryId = category.MainCategoryId;
            editcategory.CompaignId = category.CompaignId;

            string imagePath = "";
            string imageName = "";

            try
            {
                if(Image!=null && Image.ContentLength > 0)
                {
                    imageName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    imagePath = Path.Combine(Server.MapPath("~/Content/Images/category"), imageName);
                    Image.SaveAs(imagePath);
                    editcategory.Image = imageName;
                }
            }
            catch
            {

            }

            db.SaveChanges();
            return Redirect("~/Category/Edit?id=" + category.Id);
            //return View(editcategory);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var category = db.Category.Find(id);
            category.Delete = true;
            db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}