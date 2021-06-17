using deneme.Models;
using deneme.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace deneme.Auth
{
    public class UyelerService
    {
        Database1Entities db = new Database1Entities();

        public uyeModel UyeOturumAc(string kadi, string parola)
        {
            uyeModel uye = db.uyeler.Where(s => s.uyeKullaniciAdi == kadi && s.uyeParola == parola).Select(x => new uyeModel()
            {
                uyeId = x.uyeId,
                uyeAdSoyad = x.uyeAdSoyad,
                uyeMail = x.uyeMail,
                uyeKullaniciAdi = x.uyeKullaniciAdi,
                uyeParola = x.uyeParola,
                uyeAdmin = x.uyeAdmin
            }).SingleOrDefault();
            return uye;

        }
    }
}