using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Decathlon.Models;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using System.Web.UI.WebControls;
using System.Net;


namespace Decathlon.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            
            return View(GetCart());
        }
        [HttpPost]
        public ActionResult AddToCart(int id,int quantity)
        {
            var product = db.Product.FirstOrDefault(x => x.Id == id);

            if (product != null)
            {
                if (product.Stock >= quantity)
                {
                    GetCart().AddProduct(product, quantity);
                }
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpPost]
        public ActionResult DeleteFromCart(int id)
        {
            var product = db.Product.FirstOrDefault(x => x.Id == id);

            if (product != null)
            {

                GetCart().DeleteProduct(product);
               
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        [HttpPost]
        public ActionResult ClearCart()
        {
            GetCart().Clear();

            return Redirect(Request.UrlReferrer.ToString());
        }
        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];

            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult CartDropDown()
        {

            return PartialView(GetCart());
        }
        [Authorize]
        public ActionResult Checkout()
        {
            
            return View();
        } 
        [HttpPost]
        public ActionResult Checkout(Order order)
        {
            var cart = GetCart();

            if (cart.CartRows.Count != 0)
            {
                SaveOrder(cart, order);
                ViewBag.mesaj = "Sipariş Onaylandı.";
                return RedirectToAction("Payment");
                //cart.Clear();
            }
            else
            {
                return RedirectToAction("Shop", "Home");
            }
            return View();
        }
        public void SaveOrder(Cart cart,Order order)
        {
            var neworder = new Order();

            neworder.OrderNo = "ON-" + Guid.NewGuid().ToString().Substring(0, 10);
            neworder.Date = DateTime.Now;
            neworder.Total = cart.Total();
            neworder.UserId = Convert.ToInt32(User.Identity.Name);

            neworder.AddressHeader = order.AddressHeader;
            neworder.MyAddress = order.MyAddress;
            neworder.City = order.City;
            neworder.Notes = order.Notes;
            neworder.OrderStatus = "Yeni Sipariş";
            neworder.OrderLines = new List<OrderLine>();

            foreach (var productlist in neworder.OrderLines)
            {
                var orderline = new OrderLine();
                orderline.Quantity = productlist.Quantity;
                orderline.Price = productlist.Price;
                orderline.ProductId = productlist.ProductId;

                neworder.OrderLines.Add(orderline);
                
            }
            db.Order.Add(neworder);
            db.SaveChanges();

            Order orderid = db.Order.ToList().LastOrDefault();
            OrderLine orderlines;
            foreach (var orderproduct in cart.CartRows)
            {
                orderlines = new OrderLine();

                orderlines.OrderId = orderid.Id;
                orderlines.Quantity = orderproduct.Quantity;
                orderlines.Price = orderproduct.Product.Price;
                orderlines.ProductId = orderproduct.Product.Id;

                db.OrderLine.Add(orderlines);
            }
            db.SaveChanges();

            
        }
        [Authorize]
        public ActionResult Payment()
        {
            var cart = GetCart();
           
            string tutartoplam = Convert.ToString(Math.Round(cart.Total(),2));
            string tutar = tutartoplam.Replace(",",".");

            int id = Convert.ToInt32(User.Identity.Name);
            var order = db.Order.ToList().Where(x => x.UserId == id).LastOrDefault();

            Options options = new Options();
            options.ApiKey = "sandbox-FJrGsn4FoeCqodWWutCZbTyoOqIGRrIA";
            options.SecretKey = "sandbox-yuqCTZGS1jw29VDoHTE8r6rolJTsjHJr";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreateCheckoutFormInitializeRequest request = new CreateCheckoutFormInitializeRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = order.Id.ToString();
            request.Price = tutar.ToString();//ürün fiyatı
            request.PaidPrice = tutar.ToString();//varsa vergi, kargo yada hizmet bedeli eklenmiş hali
            request.Currency = Currency.TRY.ToString();
            request.BasketId = order.OrderNo;
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = "https://localhost:44353/Cart/Complated";

            //List<int> enabledInstallments = new List<int>();
            //enabledInstallments.Add(2);
            //enabledInstallments.Add(3);
            //enabledInstallments.Add(6);
            //enabledInstallments.Add(9);
            //request.EnabledInstallments = enabledInstallments;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = "John";
            buyer.Surname = "Doe";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "email@email.com";
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Iyzipay.Model.Address shippingAddress = new Iyzipay.Model.Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Iyzipay.Model.Address billingAddress = new Iyzipay.Model.Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();

            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = order.OrderNo.ToString();
            firstBasketItem.Name = "Muhtelif Ürün";
            firstBasketItem.Category1 = "Spor";
            firstBasketItem.Category2 = "";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = tutar.ToString();
            basketItems.Add(firstBasketItem);
            


            request.BasketItems = basketItems;
           
            CheckoutFormInitialize checkoutFormInitialize = CheckoutFormInitialize.Create(request, options);
            ViewBag.pay = checkoutFormInitialize.CheckoutFormContent;

            if (checkoutFormInitialize.Status == "Success")
            {
                return RedirectToAction("Complated");
            }
            return View();
        }
        public ActionResult Complated()
        {
            return View();
        }
    }
}