using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tez.MvcWebUI.Entity;
using Tez.MvcWebUI.Models;

namespace Tez.MvcWebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext db = new DataContext();
        // GET: Cart
        public ActionResult Index()
        {
            return View(GetCart());
        }

        public ActionResult AddToCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int Id)
        {
            var product = db.Products.FirstOrDefault(i => i.Id == Id);

            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }

            return RedirectToAction("Index");
        }

        public ActionResult ClearCart()
        {
            var cart = (Cart)Session["Cart"];

            cart?.CartLines.Clear();

            return RedirectToAction("Index");
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

        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }

        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ActionResult Checkout(ShippingDetails entity)
        {
            var cart = GetCart();

            if (cart.CartLines.Count == 0)
            {
                ModelState.AddModelError("UrunYokError", "Sepetinizde ürün bulunmamaktadır!");
                return View(entity);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    SaveOrder(cart, entity);
                    cart.Clear();
                    return View("Completed");
                }
                else
                {
                    return View(entity);
                }
            }
        }

        private void SaveOrder(Cart cart, ShippingDetails entity)
        {
            var order = new Order
            {
                OrderNumber = "A" + new Random().Next(11111, 99999).ToString(),
                Total = cart.Total(),
                OrderDate = DateTime.Now,
                OrderState = EnumOrderState.Waiting,
                Username = User.Identity.Name,


                Ad = entity.Ad,
                Soyad = entity.Soyad,
                Numara = entity.Numara,
                AdresBasligi = entity.AdresBasligi,
                Adres = entity.Adres,
                Sehir = entity.Sehir,
                Ilce = entity.Ilce,
                PostaKodu = entity.PostaKodu,

                OrderLines = new List<OrderLine>()
            };

            foreach (var pr in cart.CartLines)
            {
                var orderline = new OrderLine
                {
                    Quantity = pr.Quantity,
                    Price = pr.Quantity * pr.Product.Price,
                    ProductId = pr.Product.Id
                };

                order.OrderLines.Add(orderline);
            }
            db.Orders.Add(order);
            db.SaveChanges();
        }
    }
}