using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI.WebControls;

namespace Tez.MvcWebUI.Entity
{
    public class DataInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            var kategoriler = new List<Category>()
            {
                new Category(){ Name = "Mouse", Description = "Mouse Ürünleri" },
                new Category(){ Name = "Klavye", Description = "Klavye Ürünleri" },
                new Category(){ Name = "Bilgisayar", Description = "Bilgisayar Ürünleri" },
                new Category(){ Name = "Kulaklık", Description = "Kulaklık Ürünleri" },
                new Category(){ Name = "Oyun", Description = "Oyun Ürünleri" }
            };

            foreach (var kategori in kategoriler)
            {
                context.Categories.Add(kategori);
            }
            context.SaveChanges();

            var urunler = new List<Product>()
            {
                new Product(){ Name = "Logitech G305 Kablosuz Mouse", Price = 550, Code = "20", IsApproved = true, CategoryId = 1, IsHome = true, Image = "1.jpg" },
                new Product(){ Name = "Corsair K95 RGB Platinum Klavye", Price = 1200, Code = "15", IsApproved = true, CategoryId = 2, IsHome = true, Image = "2.jpg" },
                new Product(){ Name = "Apple iMac 27-inch Bilgisayar", Price = 24000, Code = "5", IsApproved = true, CategoryId = 3, Image = "3.jpg" },
                new Product(){ Name = "Sony WH-1000XM4 Kulaklık", Price = 2500, Code = "10", IsApproved = false, CategoryId = 4, IsHome = true, Image = "4.jpg" },
    };

            foreach (var urun in urunler)
            {
                context.Products.Add(urun);
            }
            context.SaveChanges();



            base.Seed(context);
        }
    }
}