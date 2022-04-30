using emlakprjsonins.Models;
using emlakprjsonins.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace emlakprjsonins.Controllers
{
    public class ServisController : ApiController
    {
        Database001Entities db = new Database001Entities();
        SonucModel sonuc = new SonucModel();

        #region Kategori
        [HttpGet]
        [Route("api/kategorilistele")]
        public List<KategoriModel> KategoriListe()
        {
            List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
            {
                kategoriId = x.kategoriId,
                kategoriAd = x.kategoriAd,
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/kategoribyid/{KategoriId}")]
        public KategoriModel KategoriById(int kategoriId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.kategoriId == kategoriId).Select(x => new KategoriModel()
            {
                kategoriId = x.kategoriId,
                kategoriAd = x.kategoriAd

            }).FirstOrDefault();

            return kayit;
        }
        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.kategoriAd == model.kategoriAd) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Kategori Adı Kayıtlıdır!";
                return sonuc;
            }
            Kategori yeni = new Kategori();
            yeni.kategoriAd = model.kategoriAd;
            db.Kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/kategoriduzenle")]
        public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.kategoriId == model.kategoriId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı!";
                return sonuc;
            }
            kayit.kategoriAd = model.kategoriAd;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/kategorisil/{KategoriId}")]

        public SonucModel KategoriSil(int kategoriId)
        {
            Kategori Kayit = db.Kategori.Where(s => s.kategoriId == kategoriId).FirstOrDefault();
            if (Kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı!";
                return sonuc;
            }

            if (db.Emlak.Count(s => s.emlakKatId == kategoriId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Emlak Bulunan Kategori Silinemez";
                return sonuc;
            }
            db.Kategori.Remove(Kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";
            return sonuc;
        }

        #endregion

        #region Emlak

        [HttpGet]
        [Route("api/emlaklistele")]

        public List<EmlakModel> AnketListe()
        {
            List<EmlakModel> liste = db.Emlak.Select(x => new EmlakModel()
            {
                emlakId = x.emlakId,
                emlakAdi = x.emlakAdi,
                emlakAciklama = x.emlakAciklama,
                emlakFiyat = x.emlakFiyat,
                emlakKatId = x.emlakKatId,
                emlakUyeId = x.emlakUyeId
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/emlaklistebykatid/{kategoriId}")]
        public List<EmlakModel> EmlakListeByKatId(int KategoriId)
        {
            List<EmlakModel> liste = db.Emlak.Where(s => s.emlakKatId == KategoriId).Select(x =>
           new EmlakModel()
           {
               emlakId = x.emlakId,
               emlakAdi = x.emlakAdi,
               emlakAciklama = x.emlakAciklama,
               emlakFiyat = x.emlakFiyat,
               emlakKatId = x.emlakKatId,
               emlakUyeId = x.emlakUyeId
           }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/emlakbyid/{emlakId}")]
        public List<EmlakModel> EmlakListeById(int emlakId)
        {
            List<EmlakModel> liste = db.Emlak.Where(s => s.emlakId == emlakId).Select(x =>
           new EmlakModel()
           {
               emlakId = x.emlakId,
               emlakAdi = x.emlakAdi,
               emlakAciklama = x.emlakAciklama,
               emlakFiyat = x.emlakFiyat,
               emlakKatId = x.emlakKatId,
               emlakUyeId = x.emlakUyeId
           }).ToList();
            return liste;
        }
        [HttpPost]
        [Route("api/emlakekle")]
        public SonucModel EmlakEkle(EmlakModel model)
        {
            if (db.Emlak.Count(s => s.emlakAdi == model.emlakAdi && s.emlakKatId == model.emlakKatId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Emlak İlgili Kategoride Kayıtlıdır!";
                return sonuc;
            }
            Emlak yeni = new Emlak();
            yeni.emlakId = model.emlakId;
            yeni.emlakAdi = model.emlakAdi;
            yeni.emlakAciklama = model.emlakAciklama;
            yeni.emlakFiyat = model.emlakFiyat;
            yeni.emlakKatId = model.emlakKatId;
            yeni.emlakUyeId = model.emlakUyeId;
            db.Emlak.Add(yeni);       
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Emlak Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/emlakduzenle")]
        public SonucModel EmlakDuzenle(EmlakModel model)
        {
            Emlak kayit = db.Emlak.Where(s => s.emlakId == model.emlakId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Emlak Bulunamadı!";
                return sonuc;
            }
            kayit.emlakAdi = model.emlakAdi;
            kayit.emlakKatId = model.emlakKatId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Emlak Düzenlendi";
            return sonuc;


        }
        [HttpDelete]
        [Route("api/emlaksil/{emlakId}")]
        public SonucModel EmlakSil(int emlakId)
        {
            Emlak kayit = db.Emlak.Where(s => s.emlakId == emlakId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Emlak Bulunamadı!";
                return sonuc;
            }
            if (db.Foto.Count(s => s.fotoEmlakId == emlakId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üzerinde Fotğraf Olan Emlak Silinemez!";
                return sonuc;
            }

            db.Emlak.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Emlak Silindi";
            return sonuc;
        }

        #endregion

        #region Foto

        [HttpGet]
        [Route("api/fotolistele")]

        public List<FotoModel> SoruListe()
        {
            List<FotoModel> liste = db.Foto.Select(x => new FotoModel()
            {
                fotoId = x.fotoId,
                fotoEmlakId = x.fotoEmlakId,
                fotolar = x.fotolar,
                fotoUyeId = x.fotoUyeId
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/fotolistebyemlakid/{emlakId}")]
        public List<FotoModel> fotoListeByEmlakId(int emlakId)
        {
            List<FotoModel> liste = db.Foto.Where(s => s.fotoEmlakId == emlakId).Select(x =>
             new FotoModel()
             {
                 fotoId = x.fotoId,
                 fotoEmlakId = x.fotoEmlakId,
                 fotolar = x.fotolar,
                 fotoUyeId = x.fotoUyeId
             }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/fotobyid/{fotoId}")]
        public List<FotoModel> FotoListeById(int fotoId)
        {
            List<FotoModel> liste = db.Foto.Where(s => s.fotoId == fotoId).Select(x =>
           new FotoModel()
           {
               fotoId = x.fotoId,
               fotoEmlakId = x.fotoEmlakId,
               fotolar = x.fotolar,
               fotoUyeId = x.fotoUyeId

           }).ToList();
            return liste;
        }
        [HttpPost]
        [Route("api/fotoekle")]
        public SonucModel FotoEkle(FotoModel model)
        {
            if (db.Foto.Count(s => s.fotolar == model.fotolar && s.fotoEmlakId == model.fotoEmlakId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Fotoğraf İlgili Emlakta Kayıtlıdır!";
                return sonuc;
            }
            Foto yeni = new Foto();
            yeni.fotoId = model.fotoId;
            yeni.fotolar = model.fotolar;
            yeni.fotoUyeId = model.fotoUyeId;
            yeni.fotoEmlakId = model.fotoEmlakId;
            db.Foto.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Fotoğraf Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/fotoduzenle")]
        public SonucModel FotoDuzenle(FotoModel model)
        {
            Foto kayit = db.Foto.Where(s => s.fotoId == model.fotoId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Fotoğraf Bulunamadı!";
                return sonuc;
            }
            kayit.fotolar = model.fotolar;
            kayit.fotoEmlakId = model.fotoEmlakId;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Fotoğraf Düzenlendi";
            return sonuc;


        }
        [HttpDelete]
        [Route("api/fotosil/{fotoId}")]
        public SonucModel FotoSil(int fotoId)
        {
            Foto kayit = db.Foto.Where(s => s.fotoId == fotoId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Fotoğraf Bulunamadı!";
                return sonuc;
            }

            db.Foto.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Fotoğraf Silindi";
            return sonuc;
        }

        #endregion

        #region Uye

        [HttpGet]
        [Route("api/uyelistele")]

        public List<UyeModel> UyeListe()
        {
            List<UyeModel> liste = db.Uye.Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                uyeAdSoyad = x.uyeAdSoyad,
                uyeEposta = x.uyeEposta,
                uyeYetki = x.uyeYetki,
                uyeParola = x.uyeParola

            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public List<UyeModel> UyeListeById(int uyeId)
        {
            List<UyeModel> liste = db.Uye.Where(s => s.uyeId == uyeId).Select(x =>
           new UyeModel()
           {
               uyeId = x.uyeId,
               uyeAdSoyad = x.uyeAdSoyad,
               uyeEposta = x.uyeEposta,
               uyeYetki = x.uyeYetki,
               uyeParola = x.uyeParola

           }).ToList();
            return liste;
        }
        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyeModel model)
        {
            if (db.Uye.Count(s => s.uyeEposta == model.uyeEposta) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Üye  Kayıtlıdır!";
                return sonuc;
            }
            Uye yeni = new Uye();
            yeni.uyeAdSoyad = model.uyeAdSoyad;
            yeni.uyeEposta = model.uyeEposta;
            yeni.uyeYetki = model.uyeYetki;
            yeni.uyeParola = model.uyeParola;
            db.Uye.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Eklendi";

            return sonuc;
        }
        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(UyeModel model)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == model.uyeId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadı!";
                return sonuc;
            }
            //kayit.UyeId = model.UyeId;
            kayit.uyeAdSoyad = model.uyeAdSoyad;
            kayit.uyeEposta = model.uyeEposta;
            kayit.uyeYetki = model.uyeYetki;
            kayit.uyeParola = model.uyeParola;
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Üye Düzenlendi";
            return sonuc;

        }
        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public SonucModel UyeSil(int uyeId)
        {
            Uye kayit = db.Uye.Where(s => s.uyeId == uyeId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadı!";
                return sonuc;
            }

            db.Uye.Remove(kayit);
            db.SaveChanges();

            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi";
            return sonuc;
        }

        #endregion
    
}
}
