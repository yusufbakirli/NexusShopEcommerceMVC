using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tez.MvcWebUI.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Features { get; set; }
        public double Price { get; set; }
        public string Code { get; set; }
        public string Image { get; set; }
        public int FavoriteCount { get; set; }
        public int CategoryId { get; set; }
    }
}