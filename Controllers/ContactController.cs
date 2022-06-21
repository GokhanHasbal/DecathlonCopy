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
    public class ContactController : Controller
    {
        DataContext db = new DataContext();
        // GET: Contact
        public ActionResult Index()
        {
            var contact = db.Contact.Find(1);
            return View(contact);
        }
        [HttpPost]
        public ActionResult Index(Contact contact, HttpPostedFileBase Image)
        {
            var editcontact = db.Contact.Find(contact.Id);
            string ImagePath = "";
            string ImageName = "";

            try
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    ImageName = Guid.NewGuid().ToString().Substring(0, 10) + "-" + Path.GetFileName(Image.FileName);
                    ImagePath = Path.Combine(Server.MapPath("~/Content/Images/contact"), ImageName);
                    Image.SaveAs(ImagePath);
                    editcontact.Image = ImageName;
                }

                editcontact.Address1 = contact.Address1;
                editcontact.Address2 = contact.Address2;
                editcontact.Phone1 = contact.Phone1 ;
                editcontact.Phone2 = contact.Phone2;
                editcontact.Email1 = contact.Email1;
                editcontact.Email2 = contact.Email2;
                editcontact.Description = contact.Description;
                editcontact.Facebook = contact.Facebook;
                editcontact.Twitter = contact.Twitter;
                editcontact.Instagram = contact.Instagram;
                editcontact.Youtube = contact.Youtube;
                editcontact.Linkedin = contact.Linkedin;
                editcontact.Pinterest = contact.Pinterest;
                editcontact.GoogleMaps = contact.GoogleMaps;
                editcontact.Status = contact.Status;
                
                db.SaveChanges();
                return View(editcontact);
            }
            catch
            {
                ViewBag.mesaj = "Beklenmeyen bir hata oluştu lütfen tekrar deneyiniz";
            }
            return View(editcontact);
        }
    }
}