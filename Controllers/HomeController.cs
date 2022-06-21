using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decathlon.Models;
using Decathlon.AdminModel;

namespace Decathlon.Controllers
{
    public class HomeController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            var product = db.Product.Where(x => x.Status == true && x.Delete == false).ToList();
            return View(product);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Shop()
        {
            var product = db.Product.Where(x => x.Status == true && x.Delete == false && x.Stock>0).ToList();
            return View(product);
        }

        public ActionResult ProductDetail(int id)
        {
            ProductModel model = new ProductModel();

            var product = db.Product.Find(id);

            if (product != null)
            {
                model.SingleProduct = product;
                model.ProductComment = db.ProductComment.Where(x => x.ProductId == product.Id && x.Status == true && x.Delete == false).ToList();
                model.ProductImage = db.ProductImage.Where(x => x.ProductId == product.Id && x.Status == true && x.Delete == false).ToList();
                model.User = db.User.ToList();
                return View(model);
            }
            else
            {
                return RedirectToAction("Shop");
            }

            
        }
       
    }
}