using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Tez.MvcWebUI.Entity;
using Tez.MvcWebUI.Models;

namespace Tez.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _context = new DataContext();

        // GET: Home
        public ActionResult Index()
        {
            // Ürünleri favorilenme sayısına göre azalan sırada getiriyoruz
            var urunler = _context.Products
                .Where(i => i.IsHome && i.IsApproved)
                .OrderByDescending(i => i.FavoriteCount) // Favorilenme sayısına göre sıralama
                .Select(i => new ProductModel()
                {
                    Id = i.Id,
                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                    Price = i.Price,
                    Code = i.Code,
                    Image = i.Image ?? "1.jpg",
                    CategoryId = i.CategoryId,
                    FavoriteCount = i.FavoriteCount // Favorilenme sayısını ekleyin
                })
                .ToList();

            return View(urunler);
        }

        public ActionResult Details(int id)
        {
            // Ürün detaylarını getir
            var product = _context.Products.FirstOrDefault(i => i.Id == id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        public ActionResult List(int? id, string query)
        {
            // Onaylanmış ürünleri filtrele
            var urunler = _context.Products
                .Where(i => i.IsApproved)
                .Select(i => new ProductModel()
                {
                    Id = i.Id,
                    Name = i.Name.Length > 50 ? i.Name.Substring(0, 47) + "..." : i.Name,
                    Price = i.Price,
                    Code = i.Code,
                    Image = i.Image ?? "1.jpg",
                    FavoriteCount = i.FavoriteCount, // Favori sayısını ekledik
                    CategoryId = i.CategoryId
                }).AsQueryable();

            // Kategori filtreleme
            if (id.HasValue)
            {
                urunler = urunler.Where(i => i.CategoryId == id);
            }

            // Arama işlemi
            if (!string.IsNullOrEmpty(query))
            {
                urunler = urunler.Where(i => i.Name.Contains(query));
            }

            return View(urunler.ToList());
        }

        public PartialViewResult GetCategories()
        {
            // Kategorileri getir
            var categories = _context.Categories.ToList();
            return PartialView(categories);
        }

        public PartialViewResult GetTopFavoritedProducts()
        {
            // Sadece favorilenme sayısına göre sıralanmış ilk 5 ürünü döndürüyoruz
            var topProducts = _context.Products
                .Where(i => i.FavoriteCount > 0) // Favorilenmiş ürünler
                .OrderByDescending(i => i.FavoriteCount) // Azalan sırada sıralama
                .Take(5) // İlk 5 ürünü al
                .Select(i => new ProductModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Price = i.Price,
                    Code = i.Code,
                    Image = i.Image ?? "1.jpg",
                    CategoryId = i.CategoryId,
                    FavoriteCount = i.FavoriteCount
                })
                .ToList();

            return PartialView("_TopFavoritedSlider", topProducts);
        }
    }
}
