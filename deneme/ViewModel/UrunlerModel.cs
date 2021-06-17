using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace deneme.ViewModel
{
    public class UrunlerModel
    {

        public string urunId { get; set; }
        public string urunAdı { get; set; }
        public decimal urunFiyatı { get; set; }
        public int urunKatId { get; set; }
        public string urunKatAdı { get; set; }
    }
}