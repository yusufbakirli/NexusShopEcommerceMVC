using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tez.MvcWebUI.Entity
{
    public enum EnumOrderState
    {
        [Display(Name = "Onay Bekleniyor")]
        Waiting,
        [Display(Name = "Hazırlanıyor")]
        Confirmed,
        [Display(Name = "Kargoya Verildi")]
        Shipped,
        [Display(Name = "Teslim Edildi")]
        Completed
    }
}