using Decathlon.Models;
using Decathlon.AdminModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Decathlon.Controllers
{
    [Authorize]
    public class UserAddressesController : Controller
    {
        DataContext db = new DataContext();
        // GET: UserAddresses
        public ActionResult Index()
        {
            AddressModel model = new AddressModel();
            int userid = Convert.ToInt32(User.Identity.Name);
            model.Address = db.Address.Where(x =>x.Delete == false && x.UserId== userid).ToList();
            model.User=db.User.Where(x => x.Delete == false && x.Id== userid).ToList();
            return View(model);
        }
        public ActionResult Select(int id)
        {
            AddressModel model = new AddressModel();
            int userid = Convert.ToInt32(User.Identity.Name);

            model.Address = db.Address.Where(x => x.Delete == false && x.UserId == userid).ToList();

            foreach (var adress in model.Address)
            {
                adress.Selection = false;
            }

            var selectadress = model.Address.FirstOrDefault(x=>x.Id==id);
            selectadress.Selection = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Address address)
        {
            int userid = Convert.ToInt32(User.Identity.Name);
            Address newaddress = new Address();

            newaddress = address;
            newaddress.UserId = userid;
            newaddress.Status = true;

            db.Address.Add(newaddress);
            db.SaveChanges();
            ViewBag.mesaj = "Adres Ekleme Başarılı";

            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var address = db.Address.Find(id);
            if (address != null)
            {
                return View(address);
            }
            else
            {
                return RedirectToAction("index");
            }
            
        }
        [HttpPost]
        public ActionResult Edit(Address address)
        {
            
            var editaddress = db.Address.Find(address.Id);
            editaddress.Title = address.Title;
            editaddress.Country = address.Country;
            editaddress.County = address.County;
            editaddress.District = address.District;
            editaddress.Street = address.Street;
            editaddress.PostCode = address.PostCode;
            editaddress.BuildingNo = address.BuildingNo;
            editaddress.ApartmentNo = address.ApartmentNo;
            editaddress.OpenAddress = address.OpenAddress;
            editaddress.Status = address.Status;

            db.SaveChanges();
            return Redirect("~/UserAddresses/Edit?id="+address.Id);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var address=db.Address.Find(id);

            address.Delete = true;
            db.SaveChanges();

            return RedirectToAction("index");
        }
    }
}