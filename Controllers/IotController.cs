using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decathlon.Models;

namespace Decathlon.Controllers
{
    public class IotController : Controller
    {
        DataContext db = new DataContext();
        // GET: Iot
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Index(string DeviceId,string enlem, string boyulam, int rakim, bool calisma)
        {
            Permission permission = new Permission()
            {
                Name = DeviceId,
                Status = calisma,
                Delete=false,
                
            };
            db.Permission.Add(permission);
            db.SaveChanges();
            return View();
        }
    }
}