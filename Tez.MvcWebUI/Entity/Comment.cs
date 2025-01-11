using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tez.MvcWebUI.Entity
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserName { get; set; }  // Yorumu yapan kişi
        public string Content { get; set; }  // Yorum metni
        public DateTime Date { get; set; } = DateTime.Now; // Yorum tarihi

        // İlişki
        public int ProductId { get; set; }  // Hangi ürüne ait olduğunu belirtir
        public Product Product { get; set; }
    }
}