using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Tez.MvcWebUI.Entity
{
    public class Product
    {
        public int Id { get; set; }

        [DisplayName("Ürün Adı")]
        public string Name { get; set; }

        [DisplayName("Teknik Özellikler")]
        public string Features { get; set; }

        [DisplayName("Fiyat")]
        public double Price { get; set; }

        [DisplayName("Ürün Kodu")]
        public string Code { get; set; }

        [DisplayName("Resim")]
        public string Image { get; set; }

        [DisplayName("Vitrinde mi?")]
        public bool IsHome { get; set; }

        [DisplayName("Stokta mı?")]
        public bool IsApproved { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int FavoriteCount { get; set; } // Favorilere eklenme sayısı

        // Yorumlarla ilişki
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}