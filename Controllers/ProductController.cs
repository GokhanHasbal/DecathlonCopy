using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decathlon.Models;
using Decathlon.AdminModel;
using System.IO;

namespace Decathlon.Controllers
{
    [Authorize(Roles ="Admin,User")]
    public class ProductController : Controller
    {
        DataContext db = new DataContext();
        // GET: Product
        public ActionResult Index()
        {
            ProductModel pm = new ProductModel();
            pm.Product = db.Product.Where(x => x.Delete == false).ToList();
            pm.Category = db.Category.Where(x => x.Delete == false).ToList();
            pm.Brand = db.Brand.Where(x => x.Delete == false).ToList();

            var productcomment = db.ProductComment.ToList().Count();
            if(productcomment!=0)
            {
                pm.ProductComment = db.ProductComment.Where(x => x.Delete == false).ToList();
            }

            pm.Campaing = db.Campaing.Where(x => x.Delete == false).ToList();
           
            return View(pm);
        }
        [HttpGet]
        public ActionResult Add()
        {
            ProductModel model = new ProductModel()
            {
                Category = db.Category.Where(x => x.MainCategoryId == 0 && x.Status==true && x.Delete==false).ToList(),
                SubCategory = db.Category.Where(x => x.MainCategoryId != 0 && x.Status == true && x.Delete == false).ToList(),
                Campaing=db.Campaing.Where(x => x.Status == true && x.Delete == false).ToList(),
                Brand=db.Brand.Where(x => x.Status == true && x.Delete == false).ToList(),
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(Product product,string SubPrice)
        {
            string fiyat = product.Price + "," + SubPrice;
            Product newproduct = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Stock = product.Stock,
                Price = Convert.ToDouble(fiyat),
                Discount = product.Discount,
                Tax = product.Tax,
                Rate = product.Rate,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                CampaingId = product.CampaingId,
                SeoKeywords = product.SeoKeywords,
                SeoDescription = product.SeoDescription,
                SeoTitle = product.SeoTitle,
                SeoUrl = product.SeoUrl,
                Status = product.Status
            };
            db.Product.Add(newproduct);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ProductModel model = new ProductModel() {
                SingleProduct = db.Product.Find(id),
                Category = db.Category.Where(x => x.MainCategoryId == 0 && x.Status == true && x.Delete == false).ToList(),
                SubCategory = db.Category.Where(x => x.MainCategoryId != 0 && x.Status == true && x.Delete == false).ToList(),
                Campaing = db.Campaing.Where(x => x.Status == true && x.Delete == false).ToList(),
                Brand = db.Brand.Where(x => x.Status == true && x.Delete == false).ToList(),
                ProductImage = db.ProductImage.Where(x => x.ProductId == id && x.Delete == false).ToList(),
                ProductComment=db.ProductComment.Where(x => x.ProductId == id && x.Status == true && x.Delete == false).ToList(),
            };
            
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(Product product,HttpPostedFileBase Image,string SubPrice)
        {
            var editproduct = db.Product.Find(product.Id);

            string ImagePath = "";
            string ImageName = "";

            try
            {
                if(Image!=null && Image.ContentLength > 0)
                {
                    ImageName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Images/product"), ImageName);
                    Image.SaveAs(ImagePath);
                    editproduct.Image = ImageName;
                }
                
                editproduct.Name = product.Name;
                editproduct.Description = product.Description;
                editproduct.Stock = product.Stock;
                string fiyat = product.Price + "," + SubPrice;
                editproduct.Price = Convert.ToDouble(fiyat);
                editproduct.Discount = product.Discount;
                editproduct.Tax = product.Tax;
                editproduct.Rate = product.Rate;
                editproduct.SeoDescription = product.SeoDescription;
                editproduct.SeoKeywords = product.SeoKeywords;
                editproduct.SeoTitle = product.SeoTitle;
                editproduct.SeoUrl = product.SeoUrl;
                editproduct.Status = product.Status;
                editproduct.CampaingId = product.CampaingId;
                editproduct.CategoryId = product.CategoryId;
                editproduct.BrandId = product.BrandId;

                db.SaveChanges();

                return Redirect("~/Product/Edit?id=" + editproduct.Id);
            }
            catch
            {

            }

            return View();
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);
            product.Delete = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ProductComment(int id)
        {
            ProductModel model = new ProductModel(){};
            model.ProductComment = db.ProductComment.Where(x => x.ProductId == id && x.Delete==false).ToList();
            model.SingleProduct = db.Product.Find(id);
            model.User = db.User.ToList();
           
            return View(model);
        }
        [HttpGet]
        public ActionResult CommentDetail(int id)
        {
            var comment = db.ProductComment.Find(id);
            ProductModel model = new ProductModel() { };
            model.SingleProduct = db.Product.Find(comment.ProductId);
            model.SingleUser = db.User.Find(comment.UserId);

            model.SingleComment = comment;

            if (comment.View == false)
            {
                comment.View = true;//yorum okunmuş olarak işaretlenicek
                db.SaveChanges();
            }
            
            return View(model);
        }
        [HttpPost]
        public ActionResult CommentDetail(int Id,string Status)
        {
            var comment = db.ProductComment.Find(Id);
            comment.Status = Convert.ToBoolean(Status);
            db.SaveChanges();
            return Redirect("~/Product/CommentDetail?id="+Id);
        }
        [HttpGet]
        public ActionResult CommentDelete(int id)
        {
            var comment = db.ProductComment.Find(id);
            comment.Delete = true;
            db.SaveChanges();
            return Redirect("~/Product/ProductComment?id=" + comment.ProductId);
        }
        [HttpPost]
        public ActionResult ProductImageAdd(ProductImage productImage,HttpPostedFileBase Image)
        {
            string ImagePath = "";
            string ImageName = "";
            ProductImage newimage = new ProductImage();
            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    ImageName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Images/product"), ImageName);
                    Image.SaveAs(ImagePath);
                    newimage.Image = ImageName;
                    newimage.Status = productImage.Status;
                    newimage.ProductId = productImage.ProductId;

                    db.ProductImage.Add(newimage);
                    db.SaveChanges();
                }
            }
            catch { }
            return Redirect("~/Product/Edit?id=" + productImage.ProductId);

        }
        public ActionResult ProductImageDelete(int id)
        {
            var image = db.ProductImage.Find(id);
            int productId = image.ProductId;
            image.Delete = true;
            db.SaveChanges();
            return Redirect("~/Product/Edit?id=" + productId);
        }
        public ActionResult ProductImageSatus(int id)
        {
            var image = db.ProductImage.Find(id);
            int productId = image.ProductId;
            image.Status = !image.Status;

            db.SaveChanges();
            return Redirect("~/Product/Edit?id=" + productId);
        }
        [HttpPost]
        public ActionResult AddComment(int UserId,int ProductId,int Rate,string Comment)
        {
            var newcomment = new ProductComment();

            newcomment.ProductId = ProductId;
            newcomment.UserId = UserId;
            newcomment.Comment = Comment;
            newcomment.Rate = Rate;
            newcomment.View = false;
            newcomment.Status = false;
            newcomment.Delete = false;
            newcomment.CommentId = 0;
            newcomment.Date = DateTime.Now;

            db.ProductComment.Add(newcomment);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}