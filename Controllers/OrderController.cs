using Decathlon.AdminModel;
using Decathlon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Decathlon.Controllers
{
    [Authorize(Roles ="Admin")]
    public class OrderController : Controller
    {
        DataContext db = new DataContext();
        // GET: Order
        public ActionResult Index()
        {
            OrderModel model = new OrderModel
            {
                Orders = db.Order.ToList(),
                OrderLines = db.OrderLine.ToList(),
                Products = db.Product.ToList(),
                Users = db.User.ToList()
            };

            return View(model);
        }
        public ActionResult Detail(int id)
        {
            var order = db.Order.Find(id);
            if (order != null)
            {
                OrderModel model = new OrderModel();
                model.SingleOrder = order;
                model.OrderLines = db.OrderLine.Where(x => x.OrderId == order.Id).ToList();
                model.SingleUser = db.User.Find(order.UserId);
                model.Products = db.Product.ToList();

                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
           
            
        }
        [HttpPost]
        public ActionResult Detail(Order order)
        {
            var editorder = db.Order.Find(order.Id);

            editorder.Status = order.Status;
            editorder.OrderStatus = order.OrderStatus;
            db.SaveChanges();

            return Redirect(Request.UrlReferrer.ToString());
        }
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}