using Decathlon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decathlon.AdminModel;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace Decathlon.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        DataContext db = new DataContext();
        // GET: User
        public UserModel model()
        {
            UserModel model = new UserModel()
            {
                User = db.User.Where(x => x.Delete == false).ToList(),
                Permission = db.Permission.Where(x => x.Delete == false).ToList(),
                Grade = db.Grade.Where(x => x.Delete == false).ToList()
            };
            return model;
        }
        public ActionResult Index()
        {
            return View(model());
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View(model());
        }
        [HttpPost]
        public ActionResult Add(User user)
        {
            var emailcontrol = db.User.FirstOrDefault(m => m.Email == user.Email);
            if (emailcontrol == null)
            {
                var fromAddress = new MailAddress("testmailhere@gmail.com");
                var toAddress = new MailAddress(user.Email);
                var subject = "Decathlon | Yeni Kayıt Bilgilendirme";
                var code = "Decathlon-" + Guid.NewGuid().ToString().Substring(0, 5);

                User newuser = new User();

                newuser = user;
                newuser.RegistrationDate = DateTime.Now;
                newuser.GradeId = 1;
                newuser.Password = code;
                db.User.Add(newuser);
                db.SaveChanges();

                try
                {

                    using (var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,//Port=25,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, "testmailpasswordhere"),
                        Timeout = 30000
                    })
                    {
                        using (var message = new MailMessage(fromAddress, toAddress)
                        {
                            Subject = subject,
                            Body = "Merhaba " + user.Email + ", kullanıcı kaydınız başarılı bir şekilde gerçekleşti <br> Şifreniz=" + code + "<br>Giriş yapmak için <a href='https://localhost:44353/Admin/Login'>tıklayınız</a>",
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
                    return View(model());
                }

            }
            else
            {
                ViewBag.mesaj = user.Email + " adresi sistemde mevcut başka bir email adresi deneyiniz.";
                return View(model());
            }
            var lastuser = db.User.ToList().LastOrDefault();
            return Redirect("~/User/Edit?id=" + lastuser.Id);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = db.User.Find(id);

            if (user != null)
            {
                ViewBag.permission = model().Permission.ToList();
                ViewBag.grade = model().Grade.ToList();

                if (user.View == false)
                {
                    user.View = true;
                    db.SaveChanges();
                }
                return View(user);
            }
            else
            {
                return Redirect("~/Admin");
            }
        }
        [HttpPost]
        public ActionResult Edit(User user, HttpPostedFileBase Image)
        {
            var useredit = db.User.Find(user.Id);

            string imagepath = "";
            string imagename = "";

            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    imagename = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    imagepath = Path.Combine(Server.MapPath("~/Content/images/users"), imagename);
                    Image.SaveAs(imagepath);

                    useredit.Image = imagename;
                }

                useredit.Email = user.Email;
                useredit.Name = user.Name;
                useredit.Surname = user.Surname;
                useredit.TcNo = user.TcNo;
                useredit.Phone = user.Phone;
                useredit.PermissionId = user.PermissionId;
                useredit.GradeId = user.GradeId;
                useredit.GradeScore = user.GradeScore;
                useredit.Status = user.Status;
                useredit.Password = user.Password;

                db.SaveChanges();
                ViewBag.mesaj = "Bilgiler başarılı bir şekilde değiştirildi";

            }
            catch
            {
                ViewBag.mesaj = "Beklenmedik Bir Hata Oluştu";
            }

            return Redirect("~/User/Edit?id=" + user.Id);


        }
        public ActionResult Delete(int id)
        {
            var user = db.User.Find(id);
            user.Delete = true;
            user.Status = false;
            db.SaveChanges();
            return Redirect("~/User");
        }
    }
}
