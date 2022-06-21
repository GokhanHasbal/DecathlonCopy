using Decathlon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Decathlon.AdminModel;
using System.IO;

namespace Decathlon.Controllers
{

    public class AdminController : Controller
    {
        // GET: Admin
        DataContext db = new DataContext();

        [Authorize]
        public ActionResult Index()
        {
            IndexModel model = new IndexModel()
            {
                User = db.User.ToList(),
                UserTotalCount = db.User.Where(x => x.Delete == false).Count(),
                UserActiveCount=db.User.Where(x=>x.Status==true && x.Delete==false).Count(),
                UserPasiveCount=db.User.Where(x=>x.Status== false && x.Delete == false).Count()
                
            };

            return View(model);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            var usercontrol = db.User.FirstOrDefault(x => x.Email == user.Email && x.Password == user.Password);
            if (usercontrol != null)
            {
                FormsAuthentication.SetAuthCookie(usercontrol.Id.ToString(), false);
                ViewBag.mesaj = "Giriş Başarılı";
                return RedirectToAction("index");
            }
            else
            {
                ViewBag.mesaj = "Giriş Bilgileri Uyuşmuyor";
                return View();
            }

        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user, string Repeat)
        {
            var emailcontrol = db.User.FirstOrDefault(m => m.Email == user.Email);
            if (emailcontrol == null)
            {
                User newuser = new User();
                if (user.Password == Repeat)
                {
                    newuser = user;
                    newuser.PermissionId = 2;
                    newuser.GradeId = 1;
                    newuser.GradeScore = 0;
                    newuser.RegistrationDate = DateTime.Now;

                    db.User.Add(newuser);
                    db.SaveChanges();

                    ViewBag.mesaj = "Kayıt Başarılı";
                }
                else
                {
                    ViewBag.mesaj = "Şifre Uyuşmuyor";
                }          
            }
            else
            {
                ViewBag.mesaj = user.Email + " adresi sistemde mevcut başka bir email adresi deneyiniz.";
            }
            return View();
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Admin/Login");
        }
        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(string Email)
        {
            var emailcontrol = db.User.FirstOrDefault(m => m.Email == Email);
            if (emailcontrol != null)
            {
                var fromAddress = new MailAddress("iskuryazilim@gmail.com");
                var toAddress = new MailAddress(Email);
                var subject = "Decathlon | Şifre Hatılatma";
                var code = Guid.NewGuid().ToString().Substring(0, 8);



                try
                {
                    emailcontrol.Code = code;
                    db.SaveChanges();
                    using (var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,//Port=25,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, "iskur123."),
                        Timeout = 30000
                    })
                    {
                        using (var message = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = subject,
                            Body = "Merhaba " + emailcontrol.Email + " Şifre Yenileme Kodu=" + code + "<br><a href='https://localhost:44353/Admin/NewPassword/" + code + "'>New Password</a>",
                            IsBodyHtml = true
                        })
                        {
                            smtp.Send(message);
                        }
                    }

                }
                catch
                {
                    ViewBag.mesaj = "Beklenmedik Bir Hata Gerçekleşti. Lütfen Tekrar Deneyiniz.";
                }

            }
            else
            {
                ViewBag.mesaj = "Böyle Bir Kayıt Bulunamadı";
            }
            return View();
        }
        [HttpGet]
        [Route("{Code}")]
        public ActionResult NewPassword(string code)
        {
            var codecontrol = db.User.FirstOrDefault(x => x.Code == code);
            if (codecontrol != null)
            {
                ViewBag.code = code;
            }
            else
            {
                ViewBag.mesaj = "Böyle Bir Code Bulunamadı";
            }
            return View();
        }
        [HttpPost]
        public ActionResult NewPassword(string Password,string Repeat,string Code)
        {
            if (Password == Repeat)
            {
                var codecontrol = db.User.FirstOrDefault(x => x.Code == Code);
                codecontrol.Password = Password;
                codecontrol.Code = null;
                db.SaveChanges();
                ViewBag.mesaj = "Şifre Değiştirme Başarılı";
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.mesaj = "Şifre Aynı Değil Tekrar Deneyiniz";
                return View();
            }
            

        }
        [Authorize]
        public ActionResult Sidebar()
        {
            try
            {
                int id = Convert.ToInt32(User.Identity.Name);
                if (id > 0)
                {
                    var usercontrol = db.User.FirstOrDefault(x => x.Id == id);
                    if (usercontrol != null)
                    {
                        ViewBag.yetki = db.Permission.Find(usercontrol.PermissionId).Name;
                        return PartialView(usercontrol);
                    }
                    else
                    {
                        return RedirectToAction("login");
                    }
                }
                else
                {
                    return RedirectToAction("login");
                }
            }
            catch
            {
                return RedirectToAction("login");
            }
        }
        [HttpGet][Authorize(Roles ="Admin,User")]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult ResetPassword(string Password,string NewPassword,string RepeatPassword)
        {
            int id = Convert.ToInt32(User.Identity.Name);
            var passwordcontrol = db.User.FirstOrDefault(x => x.Password == Password && x.Id == id);
            if (passwordcontrol != null)
            {
                if (Password != NewPassword)
                {
                    if (NewPassword == RepeatPassword)
                    {
                        passwordcontrol.Password = NewPassword;
                        db.SaveChanges();
                        ViewBag.mesaj = "Şifre Değiştirme Başarılı";
                    }
                    else
                    {
                        ViewBag.mesaj = "Şifreler Uyuşmuyor";
                    }
                }
                else
                {
                    ViewBag.mesaj = "Yeni Şifre Eski Şifre Ile Aynı Olamaz";
                }
            }
            else
            {
                ViewBag.mesaj = "Şifre Doğru Değil";

            }
            return View();
        }
        [Authorize]
        public ActionResult MyAccount()
        {
            int id = Convert.ToInt32(User.Identity.Name);
            if (id > 0)
            {
                var usercontrol = db.User.FirstOrDefault(x => x.Id == id);
                if (usercontrol != null)
                {
                    ViewBag.yetki = db.Permission.Find(usercontrol.PermissionId).Name;
                    ViewBag.grade = db.Grade.Find(usercontrol.GradeId).Name;
                    return View(usercontrol);
                }
                else
                {
                    return RedirectToAction("login");
                }
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult UpdateImage(HttpPostedFileBase Image)
        {
            int id = Convert.ToInt32(User.Identity.Name);
            if (id > 0)
            {
                var user = db.User.FirstOrDefault(x => x.Id == id);

                string imagepath = "";
                string imagename = "";

                try
                {
                    if(Image!=null && Image.ContentLength > 0)
                    {
                        imagename = Guid.NewGuid().ToString().Substring(0,10) + "-" + Path.GetFileName(Image.FileName);
                        imagepath = Path.Combine(Server.MapPath("~/Content/images/users"),imagename);
                        Image.SaveAs(imagepath);

                        user.Image = imagename;
                        db.SaveChanges();
                        ViewBag.mesaj = "Resim Güncelleme Başarılı";
                    }
                }
                catch
                {
                    ViewBag.mesaj = "Beklenmedik Bir Hata Oluştu";
                }

                return RedirectToAction("MyAccount");
            }
            else
            {
                return RedirectToAction("login");
            }

        }
        [HttpGet]
        [Authorize]
        public ActionResult UpdateMyAccount()
        {
            int id = Convert.ToInt32(User.Identity.Name);
            if (id > 0)
            {
                var usercontrol = db.User.FirstOrDefault(x => x.Id == id);
                if (usercontrol != null)
                {
                    ViewBag.yetki = db.Permission.Find(usercontrol.PermissionId).Name;
                    ViewBag.grade = db.Grade.Find(usercontrol.GradeId).Name;
                    return View(usercontrol);
                }
                else
                {
                    return RedirectToAction("login");
                }
            }
            else
            {
                return RedirectToAction("login");
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult UpdateMyAccount(User user)
        {
            int id = Convert.ToInt32(User.Identity.Name);
            if (id > 0)
            {
                var usercontrol = db.User.FirstOrDefault(x => x.Id == id);
                if (usercontrol != null)
                {
                    usercontrol.Name = user.Name;
                    usercontrol.Surname = user.Surname;
                    usercontrol.TcNo = user.TcNo;
                    usercontrol.Phone = user.Phone;

                    db.SaveChanges();

                    ViewBag.yetki = db.Permission.Find(usercontrol.PermissionId).Name;
                    ViewBag.grade = db.Grade.Find(usercontrol.GradeId).Name;

                    ViewBag.mesaj = "Bilgiler Başarılı Bir Şekilde Güncellendi";

                    return View(usercontrol);
                }
                else
                {
                    return RedirectToAction("login");
                }
            }
            else
            {
                return RedirectToAction("login");

            }
        }
        public ActionResult Notification()
        {
            /*
             * Product Comment+
             * Site Comment+
             * Siparis+
             * Destek Talebi+
             * İletişim Mail+
             * Bizden Haberdar Olun+
             * Yeni Kullanıcılar+
             * 
             */
          

            var NewsGet = db.NewsGet.Where(x => x.Delete == false && x.View == false).ToList();
            var User = db.User.Where(x => x.Delete == false && x.View == false).ToList();
            var ProductComment = db.ProductComment.Where(x => x.Delete == false && x.View == false).ToList();
            var SiteComment = db.SiteComment.Where(x => x.Delete == false && x.View == false).ToList();
            var Support = db.Support.Where(x => x.Delete == false && x.View == false).ToList();
            var ContactMail = db.ContactMail.Where(x => x.Delete == false && x.View == false).ToList();
            //var Order = db.Order.Where(x =>x.View == false).ToList();

            
            List<Notification> notifications = new List<Notification>();
            Notification newnotification;

            foreach (var item in NewsGet)
            {
                newnotification = new Notification();
                newnotification.Date = item.Date;
                newnotification.Icon = "mdi-email-edit";
                newnotification.NameSurname = item.Email;
                newnotification.Content = item.Email;
                newnotification.Color = "bg-primary";
                notifications.Add(newnotification);
            }
            foreach (var item in User)
            {
                newnotification = new Notification();
                newnotification.Date = item.RegistrationDate;
                newnotification.Icon = "mdi-account";
                newnotification.NameSurname = item.Name+" "+item.Surname;
                newnotification.Content = item.Email;
                newnotification.Color = "bg-success";
                newnotification.Link = "User/Edit?id=" + item.Id;
                notifications.Add(newnotification);
            }
            foreach (var item in ProductComment)
            {
                newnotification = new Notification();
                newnotification.Date = item.Date;
                newnotification.Icon = "mdi-comment";
                newnotification.NameSurname = db.User.FirstOrDefault(x => x.Id == item.UserId).Email;
                newnotification.Content = item.Comment.Length<50?item.Comment:item.Comment.Substring(0,50);
                newnotification.Color = "bg-warning";
                newnotification.Link = "Product/CommentDetail?id=" + item.Id;
                notifications.Add(newnotification);
            }
            foreach (var item in SiteComment)
            {
                newnotification = new Notification();
                newnotification.Date = item.Date;
                newnotification.Icon = "mdi-comment-processing-outline";
                newnotification.NameSurname = db.User.FirstOrDefault(x => x.Id == item.UserID).Email;
                newnotification.Content = item.Comment.Length < 50 ? item.Comment : item.Comment.Substring(0, 50);
                newnotification.Color = "bg-danger";

                notifications.Add(newnotification);
            }
            foreach (var item in Support)
            {
                newnotification = new Notification();
                newnotification.Date = item.Date;
                newnotification.Icon = "mdi-ticket-account";
                newnotification.NameSurname = db.User.FirstOrDefault(x => x.Id == item.UserId).Email;
                newnotification.Content = item.Subject.Length < 50 ? item.Subject : item.Subject.Substring(0, 50);
                newnotification.Color = "bg-info";

                notifications.Add(newnotification);
            }
            foreach (var item in ContactMail)
            {
                newnotification = new Notification();
                newnotification.Date = item.Date;
                newnotification.Icon = "mdi-email-edit";
                newnotification.NameSurname = item.Name + " " + item.Surname;
                newnotification.Content = item.Subject.Length < 50 ? item.Subject : item.Subject.Substring(0, 50);
                newnotification.Color = "bg-secondary";

                notifications.Add(newnotification);
            }
            //foreach (var item in Order)
            //{
            //    newnotification = new Notification();
            //    newnotification.Date = item.Date;
            //    newnotification.Icon = "mdi-archive";
            //    newnotification.NameSurname = db.User.FirstOrDefault(x => x.Id == item.UserId).Email;
            //    newnotification.Content = db.Product.FirstOrDefault(x=>x.Id==item.ProductId).Name;
            //    newnotification.Color = "bg-dark";

            //    notifications.Add(newnotification);
            //}

            var model = notifications.OrderByDescending(x => x.Date).ToList();
            return PartialView(model);
        }
        public ActionResult ClearNotification()
        {
            db.NewsGet.Where(x => x.Delete == false && x.View == false).ToList().ForEach(x=>x.View=true);
            db.User.Where(x => x.Delete == false && x.View == false).ToList().ForEach(x => x.View = true);
            db.ProductComment.Where(x => x.Delete == false && x.View == false).ToList().ForEach(x => x.View = true);
            db.SiteComment.Where(x => x.Delete == false && x.View == false).ToList().ForEach(x => x.View = true);
            db.Support.Where(x => x.Delete == false && x.View == false).ToList().ForEach(x => x.View = true);
            db.ContactMail.Where(x => x.Delete == false && x.View == false).ToList().ForEach(x => x.View = true);
            //db.Order.Where(x => x.View == false).ToList().ForEach(x => x.View = true);
            db.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}