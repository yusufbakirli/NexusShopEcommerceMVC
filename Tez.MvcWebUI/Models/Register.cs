using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tez.MvcWebUI.Models
{
    public class Register
    {
        [Required]
        [DisplayName("İsim")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Soyisim")]
        public string SurName { get; set; }

        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Telefon Numarası")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "E-posta adresiniz geçerli değil.")]
        [DisplayName("E-posta")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Şifre")]
        public string Password { get; set; }

        [Required]
        [DisplayName("Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreleriniz uyuşmuyor.")]
        public string RePassword { get; set; }
    }
}