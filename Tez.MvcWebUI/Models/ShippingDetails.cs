using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tez.MvcWebUI.Models
{
    public class ShippingDetails
    {
        public string Username { get; set; }

        [Required(ErrorMessage = "Lütfen adınızı giriniz.")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Lütfen soyadınızı giriniz.")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "Lütfen telefon numaranızı giriniz.")]
        public string Numara { get; set; }

        [Required(ErrorMessage = "Lütfen adres tanımını giriniz.")]
        public string AdresBasligi { get; set; }

        [Required(ErrorMessage = "Lütfen bir adres giriniz.")]
        public string Adres { get; set; }

        [Required(ErrorMessage = "Lütfen şehrinizi giriniz.")]
        public string Sehir { get; set; }

        [Required(ErrorMessage = "Lütfen ilçenizi giriniz.")]
        public string Ilce { get; set; }

        public string PostaKodu { get; set; }
    }
}