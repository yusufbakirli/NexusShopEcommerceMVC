using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tez.MvcWebUI.Entity
{
    public class Favorite
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; } // Kullanıcı adı (favori kullanıcıya bağlı)

        public virtual List<FavoriteLine> FavoriteLines { get; set; } // Favorilerdeki ürünler
    }

    public class FavoriteLine
    {
        public int Id { get; set; }
        public int FavoriteId { get; set; }

        public virtual Favorite Favorite { get; set; }

        public int ProductId { get; set; } // Ürün ID'si
        public virtual Product Product { get; set; }
    }
}