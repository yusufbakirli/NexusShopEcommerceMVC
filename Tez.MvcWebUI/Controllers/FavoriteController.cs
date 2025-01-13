using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tez.MvcWebUI.Entity;
using Tez.MvcWebUI.Models;

namespace Tez.MvcWebUI.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private readonly DataContext db = new DataContext();

        // Favoriler sayfası
        public ActionResult Index()
        {
            var username = User.Identity.Name;
            var favorites = db.Favorites
                .Where(f => f.Username == username)
                .Select(f => new FavoriteModel
                {
                    Id = f.Id,
                    FavoriteLines = f.FavoriteLines.Select(fl => new FavoriteLineModel
                    {
                        ProductId = fl.ProductId,
                        ProductName = fl.Product.Name,
                        Price = fl.Product.Price,
                        Image = fl.Product.Image ?? "default.png"
                    }).ToList()
                }).FirstOrDefault();

            return View(favorites);
        }

        // Favorilere ürün ekle
        public ActionResult AddToFavorites(int productId)
        {
            var username = User.Identity.Name;
            var favorite = db.Favorites.FirstOrDefault(f => f.Username == username);

            if (favorite == null)
            {
                favorite = new Favorite
                {
                    Username = username,
                    FavoriteLines = new List<FavoriteLine>()
                };
                db.Favorites.Add(favorite);
            }

            if (!favorite.FavoriteLines.Any(fl => fl.ProductId == productId))
            {
                favorite.FavoriteLines.Add(new FavoriteLine
                {
                    ProductId = productId
                });
                // FavoriteCount'u güncelle
                var product = db.Products.FirstOrDefault(p => p.Id == productId);
                if (product != null)
                {
                    product.FavoriteCount++;
                }
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Favorilerden ürün kaldır
        public ActionResult RemoveFromFavorites(int productId)
        {
            var username = User.Identity.Name;
            var favorite = db.Favorites.FirstOrDefault(f => f.Username == username);

            if (favorite != null)
            {
                var favoriteLine = favorite.FavoriteLines.FirstOrDefault(fl => fl.ProductId == productId);
                if (favoriteLine != null)
                {
                    db.FavoriteLines.Remove(favoriteLine);
                    // FavoriteCount'u güncelle
                    var product = db.Products.FirstOrDefault(p => p.Id == productId);
                    if (product != null)
                    {
                        product.FavoriteCount--;
                    }
                    db.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult RemoveAndAddToCart(int productId)
        {
            var username = User.Identity.Name;
            var favorite = db.Favorites.FirstOrDefault(f => f.Username == username);

            // Favorilerden kaldır
            if (favorite != null)
            {
                var favoriteLine = favorite.FavoriteLines.FirstOrDefault(fl => fl.ProductId == productId);
                if (favoriteLine != null)
                {
                    db.FavoriteLines.Remove(favoriteLine);
                    // FavoriteCount'u güncelle
                    var prod = db.Products.FirstOrDefault(p => p.Id == productId);
                    if (prod != null)
                    {
                        prod.FavoriteCount--;
                    }
                    db.SaveChanges();
                }
            }

            // Sepete ekle
            var product = db.Products.FirstOrDefault(p => p.Id == productId);
            if (product != null)
            {
                var cart = (Cart)Session["Cart"] ?? new Cart();
                cart.AddProduct(product, 1);
                Session["Cart"] = cart;
            }

            TempData["message"] = "Ürün favorilerden kaldırılarak sepete eklendi.";
            return RedirectToAction("Index");
        }
    }
}