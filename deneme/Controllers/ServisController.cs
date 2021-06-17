using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using deneme.Models;
using deneme.ViewModel;

namespace deneme.Controllers
{
    public class ServisController : ApiController
    {
        Database1Entities db = new Database1Entities();
        SonucModel sonuc = new SonucModel();


        #region kategoriler

        [HttpGet]
        [Route("api/kategoriliste")]
        public List<kategorilerModel> kategorilerListe()
        {
            List<kategorilerModel> liste = db.kategoriler.Select(x => new kategorilerModel()
            {
               katId= x.katId,
               katAdı= x.katAdı,
               katUrunSay = x.urunler.Count()

            }).ToList();
            return liste; 
        }
        [HttpGet]
        [Route("api/kategoribyid/{katId}")]
        public kategorilerModel kategoriById(int katId)
        {
            kategorilerModel kayit = db.kategoriler.Where(s => s.katId == katId).Select(x => new kategorilerModel()
            {
                katId = x.katId,
                katAdı = x.katAdı,
                katUrunSay = x.urunler.Count()

            }).SingleOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(kategorilerModel model)
        {
            if (db.kategoriler.Count(s => s.katAdı == model.katAdı) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Adı Kayıtlıdır!";
                return sonuc;
            }

            kategoriler yeni = new kategoriler();
            yeni.katAdı = model.katAdı;
            db.kategoriler.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(kategoriler model)
        {
            kategoriler kayit = db.kategoriler.Where(s => s.katId == model.katId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı!";
                return sonuc;
            }

            kayit.katAdı = model.katAdı;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/kategorisil/{katId}")]
        public SonucModel KategoriSil(int katId)
        {
            kategoriler kayit = db.kategoriler.Where(s => s.katId == katId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı!";
                return sonuc;
            }

            if (db.urunler.Count(s => s.urunKatId == katId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Ürün Kaydı Olan Kategori Silinemez!";
                return sonuc;
            }

            db.kategoriler.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";
            return sonuc;
        }

        #endregion

        #region ürünler

        [HttpGet]
        [Route("api/urunlistele")]
        public List<UrunlerModel> UrunListe()
        {
            List<UrunlerModel> liste = db.urunler.Select(x => new UrunlerModel()
            {
                urunId = x.urunId,
                urunAdı = x.urunAdı,
                urunFiyatı = x.urunFiyatı,
                urunKatId = x.urunKatId,
                urunKatAdı = x.kategoriler.katAdı,
                

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/urunlistelebykatid/{katId}")]
        public List<UrunlerModel> UrunListeleByKatId(int katId)
        {
            List<UrunlerModel> liste = db.urunler.Where(s => s.urunKatId == katId).Select(x => new UrunlerModel()
            {
                urunId = x.urunId,
                urunAdı = x.urunAdı,
                urunKatId = x.urunKatId,
                urunKatAdı = x.kategoriler.katAdı,
                urunFiyatı = x.urunFiyatı

            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/urunbyid/{urunId}")]
        public UrunlerModel UrunById(string urunId)
        {
            UrunlerModel kayit = db.urunler.Where(s => s.urunId == urunId).Select(x => new UrunlerModel()
            {
                urunId = x.urunId,
                urunAdı = x.urunAdı,
                urunKatId = x.urunKatId,
                urunKatAdı = x.kategoriler.katAdı,
                urunFiyatı = x.urunFiyatı

            }).FirstOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/urunekle")]
        public SonucModel UrunEkle(UrunlerModel model)
        {
            if (db.urunler.Count(s => s.urunAdı == model.urunAdı && s.urunKatId == model.urunKatId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Ürün İlgili Kategoride Kayıtlıdır!";
                return sonuc;
            }

            urunler yeni = new urunler();
            yeni.urunId = Guid.NewGuid().ToString();
            yeni.urunAdı = model.urunAdı;
            yeni.urunFiyatı = model.urunFiyatı;
            yeni.urunKatId = model.urunKatId;

            db.urunler.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Ürün Eklendi";

            return sonuc;
        }

        [HttpPut]
        [Route("api/urunduzenle")]
        public SonucModel UrunDuzenle(UrunlerModel model)
        {
            urunler kayit = db.urunler.Where(s => s.urunId == model.urunId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün Bulunamadı!";
                return sonuc;
            }

            kayit.urunAdı = model.urunAdı;
            kayit.urunFiyatı = model.urunFiyatı;
            kayit.urunKatId = model.urunKatId;
            
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/urunsil/{urunId}")]
        public SonucModel OgrenciSil(string urunId)
        {
            urunler kayit = db.urunler.Where(s => s.urunId == urunId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Ürün Bulunamadı!";
                return sonuc;
            }

            db.urunler.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Silindi";
            return sonuc;
        }
        #endregion

        #region Uyeler

        [HttpGet]
        [Route("api/uyelistele")]
        public List<uyeModel> UyeListe()
        {
            List<uyeModel> liste = db.uyeler.Select(x => new uyeModel()
            {
                uyeId = x.uyeId,
                uyeKullaniciAdi = x.uyeKullaniciAdi,
                uyeMail = x.uyeMail,
                uyeParola = x.uyeParola,
                uyeAdSoyad = x.uyeAdSoyad,
                uyeAdmin = x.uyeAdmin


            }).ToList();

            return liste;
        }

        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public uyeModel UyeById(string uyeId)
        {
            uyeModel kayit = db.uyeler.Where(s => s.uyeId == uyeId).Select(x => new uyeModel()
            {
                uyeId = x.uyeId,
                uyeKullaniciAdi = x.uyeKullaniciAdi,
                uyeMail = x.uyeMail,
                uyeParola = x.uyeParola,
                uyeAdSoyad = x.uyeAdSoyad,
                uyeAdmin = x.uyeAdmin


            }).FirstOrDefault();

            return kayit;
        }

        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(uyeModel model)
        {
            if (db.uyeler.Count(s => s.uyeKullaniciAdi == model.uyeKullaniciAdi || s.uyeMail == model.uyeMail) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kullanıcı Adı veya Mail  Kayıtlıdır!";
                return sonuc;
            }

            uyeler yeni = new uyeler();
            yeni.uyeId = Guid.NewGuid().ToString();
            yeni.uyeAdSoyad = model.uyeAdSoyad;
            yeni.uyeKullaniciAdi = model.uyeKullaniciAdi;
            yeni.uyeMail = model.uyeMail;
            yeni.uyeParola = model.uyeParola;
            yeni.uyeAdmin = model.uyeAdmin;

            db.uyeler.Add(yeni);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Üye Kaydınız Oluşturuldu";

            return sonuc;
        }

        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(uyeler model)
        {
            uyeler kayit = db.uyeler.Where(s => s.uyeId == model.uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadı!";
                return sonuc;
            }

            kayit.uyeAdSoyad = model.uyeAdSoyad;
            kayit.uyeKullaniciAdi = model.uyeKullaniciAdi;
            kayit.uyeMail = model.uyeMail;
            kayit.uyeParola = model.uyeParola;
            kayit.uyeAdmin = model.uyeAdmin;


            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public SonucModel UyeSil(string uyeId)
        {
            uyeler kayit = db.uyeler.Where(s => s.uyeId == uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadı!";
                return sonuc;
            }

            db.uyeler.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi";
            return sonuc;
        }



        #endregion

        #region Sepet 


        [HttpPost]
        [Route("api/sepeteekle")]
        public SonucModel SepeteEkle(sepetModel model)
        {
            if (db.sepet.Count(s => s.sepetUyeId == model.sepetUyeId && s.sepetUrunId == model.sepetUrunId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Bu ürün sepette bulunmaktadır.!";
                return sonuc;
            }

            sepet yeni = new sepet();
            yeni.sepetId = Guid.NewGuid().ToString();
            yeni.sepetUyeId = model.sepetUyeId;
            yeni.sepetUrunId = model.sepetUrunId;
            db.sepet.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Sepete Eklendi";
            return sonuc;
        }

        [HttpGet]
        [Route("api/sepetliste/{uyeId}")]
        public List<sepetModel> SepetListe(string uyeId)
        {
            List<sepetModel> liste = db.sepet.Where(s => s.sepetUyeId == uyeId).Select(x => new sepetModel()
            {
                sepetId = x.sepetId,
                sepetUyeId = x.sepetUyeId,
                sepetUrunId = x.sepetUrunId,

            }).ToList();

            foreach (var sepet in liste)
            {
                sepet.urunBilgi = UrunById(sepet.sepetUrunId);
                sepet.uyeBilgi = UyeById(sepet.sepetUyeId);
            }
            return liste;
        }

        [HttpDelete]
        [Route("api/sepettencikar/{sepetId}")]
        public SonucModel SepettenCikar(string sepetId)
        {
            sepet sepet = db.sepet.Where(s => s.sepetId == sepetId).SingleOrDefault();

            if (sepet == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Sepet Bulunamadı!";
                return sonuc;
            }
            db.sepet.Remove(sepet);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Ürün Çıkarıldı ";
            return sonuc;
        }

        #endregion


    }
}
