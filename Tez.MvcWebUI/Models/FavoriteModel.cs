using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tez.MvcWebUI.Models
{
    public class FavoriteModel
    {
        public int Id { get; set; }
        public List<FavoriteLineModel> FavoriteLines { get; set; }
    }

    public class FavoriteLineModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    }
}