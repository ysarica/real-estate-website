using SRCBasicAdmin.Models;
using SRCBasicAdmin.Models.Arac;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SRCBasicAdmin.Controllers
{

    public class AdminController : Controller
    {
        SRCBasicDB db = new SRCBasicDB();

        // GET: Admin
        public ActionResult Index()
        {
            if (Session["kullanici"] != null)
            {
                return Redirect("/Giris/Index/");
            }
            return View();
        }

        public ActionResult Urun()
        {
            if (Session["kullanici"] != null)
            {
                return Redirect("/Giris/Index/");
            }
            return View(db.Urun.ToList());
        }
        [ValidateInput(false)]

        public ActionResult _UrunEkle(Urun ur, HttpPostedFileBase attachment)
        {
            Urun u = new Urun();

            if (attachment != null)
            {
                Random r = new Random();
                string dosyaYolu = "U-" + r.Next(1000, 99999).ToString() + Path.GetExtension(attachment.FileName);
                var yuklemeYeri = Path.Combine(Server.MapPath("/Resim/"), dosyaYolu);
                attachment.SaveAs(yuklemeYeri);
                ur.KapakFoto = "/Resim/" + dosyaYolu;
            }
            else
            {
                ur.KapakFoto = "/Resim/resim-yok.jpg";
            }
            db.Urun.Add(ur);
            db.SaveChanges();
            return Redirect("/Admin/Urun/");
        }
        [ValidateInput(false)]

        public ActionResult _UrunDuzenle(Urun ur, HttpPostedFileBase attachment)
        {
            Urun u = db.Urun.Where(x => x.IlanID == ur.IlanID).SingleOrDefault();
            u.IlanTip = ur.IlanTip;
            u.IlanBaslik = ur.IlanBaslik;
            u.Fiyat = ur.Fiyat;
            u.IlanAciklama = ur.IlanAciklama;
            u.Adres = ur.Adres;
            u.IcOzellik = ur.IcOzellik;
            u.DisOzellik = ur.DisOzellik;
            u.UlasimMuhit = ur.UlasimMuhit;
            u.EmlakTip = ur.EmlakTip;
            u.mKareBrut = ur.mKareBrut;
            u.mKareNet = ur.mKareNet;
            u.OdaSayi = ur.OdaSayi;
            u.BinaYas = ur.BinaYas;
            u.BulunduKat = ur.BulunduKat;
            u.KatSayi = ur.KatSayi;
            u.Isitma = ur.Isitma;
            u.BanyoSayi = ur.BanyoSayi;
            u.Balkon = ur.Balkon;
            u.KullanımDurum = ur.KullanımDurum;
            u.SiteIci = ur.SiteIci;
            u.Aidat = ur.Aidat;
            u.KrediDurum = ur.KrediDurum;
            u.TapuDurum = ur.TapuDurum;
            u.Takas = ur.Takas;
            u.Engelli = ur.Engelli;
            u.UrunVideo = ur.UrunVideo;

            if (attachment != null)
            {
                if (u.KapakFoto != "/Resim/resim-yok.jpg")
                {
                    System.IO.File.Delete(Server.MapPath(u.KapakFoto));

                }
                Random r = new Random();
                string dosyaYolu = "U-" + r.Next(1000, 99999).ToString() + Path.GetExtension(attachment.FileName);
                var yuklemeYeri = Path.Combine(Server.MapPath("/Resim/"), dosyaYolu);
                attachment.SaveAs(yuklemeYeri);
                u.KapakFoto = "/Resim/" + dosyaYolu;
            }
            db.SaveChanges();
            return Redirect("/Admin/Urun/");
        }
        public ActionResult _UrunSil(int IlanID)
        {
            Urun u = db.Urun.Where(x => x.IlanID == IlanID).SingleOrDefault();
            System.IO.File.Delete(Server.MapPath(u.KapakFoto));
            List<UrunFoto> f = db.UrunFoto.Where(x => x.IlanID == IlanID).ToList();
            foreach (var a in f)
            {
                UrunFoto b = db.UrunFoto.Where(x => x.FotoID == a.FotoID).SingleOrDefault();
                System.IO.File.Delete(Server.MapPath(b.Foto));
                db.UrunFoto.Remove(b);
                db.SaveChanges();
            }
            db.Urun.Remove(u);
            db.SaveChanges();
            return Redirect("/Admin/Urun/");

        }
        public ActionResult UrunDetay(int IlanID)
        {
            Session["IlanID"] = IlanID.ToString();
         
                Session["reshata"] = Session["reshata"] + "ss";
           
            return View(db.Urun.Where(x => x.IlanID == IlanID).SingleOrDefault());
        }
        [HttpPost]
        public ActionResult _uResimEkle(HttpPostedFileBase[] attachment)
        {
            string dosyaYolu="0";
            string yuklemeYeri="0";
            string hata = "hatasız";
            Session["reshata"] = "boş session";
            int s = 0;
            int IlanID = Convert.ToInt32(Convert.ToInt32(Session["IlanID"].ToString()));


            foreach (HttpPostedFileBase item in attachment)
            {s++;
                if (item != null)
                {
                    hata = hata + " - " + s + " Girdi <br />";
                    
                    UrunFoto u = new UrunFoto();
                    Random r = new Random();
                    dosyaYolu = ("U-" + r.Next(1000, 99999).ToString() + "-" + DateTime.Now.Day + "-" + DateTime.Now.Month + "-" + DateTime.Now.Year + "-" + DateTime.Now.Hour + "-" + DateTime.Now.Minute + "-" + DateTime.Now.Second + "-" + DateTime.Now.Millisecond +"-"+s+ Path.GetExtension(item.FileName)).ToString();
                    yuklemeYeri = Path.Combine(Server.MapPath("/Resim/"), dosyaYolu);
                    hata = hata + " - " + s + " Foto Adresler-"+dosyaYolu+"-"+yuklemeYeri+ "<br />";
                    item.SaveAs(yuklemeYeri);
                    UrunFoto ur = new UrunFoto();
                    ur.IlanID = IlanID;
                    ur.Foto = "/Resim/" + dosyaYolu;
                    hata = hata + " - " + s + " Foto Adresler2-" + dosyaYolu + "-" + yuklemeYeri + "<br />";
                    db.UrunFoto.Add(ur);
                    db.SaveChanges();
                    hata = hata + " - " + s + " Kaydedildi <br />";
                    dosyaYolu = "1";
                    yuklemeYeri = "1";
                    hata = hata + " - " + s + " Çıktı<br />";
                    hata = hata + " - " + s + "<br />";
                }
                else
                {
                    hata = hata + " - " + s + " Girmedi <br />";
                }
                dosyaYolu = "2";
            yuklemeYeri = "2";
            }
            Session["reshata"] = hata.ToString();
            return Redirect("/Admin/UrunDetay?IlanID=" + IlanID);
        }
        public ActionResult _uResimSil(int FotoID)
        {
            UrunFoto u = db.UrunFoto.Where(x => x.FotoID == FotoID).SingleOrDefault();
            System.IO.File.Delete(Server.MapPath(u.Foto));
            db.UrunFoto.Remove(u);
            db.SaveChanges();
            int IlanID = Convert.ToInt32(Convert.ToInt32(Session["IlanID"].ToString()));
            return Redirect("/Admin/UrunDetay?IlanID=" + IlanID);
        }
        //public ActionResult Referans()
        //{
        //    return View(db.Referans.ToList());
        //}
        //public ActionResult _ReferansEkle(Referans re)
        //{
        //    Referans r = new Referans();
        //    r.ReferansAdi = re.ReferansAdi;
        //    r.ReferansAciklama = re.ReferansAciklama;
        //    db.Referans.Add(r);
        //    db.SaveChanges();
        //    return Redirect("/Admin/Referans/");
        //}
        //public ActionResult _ReferansDuzenle(Referans re)
        //{
        //    Referans r = db.Referans.Where(x => x.RID == re.RID).SingleOrDefault();
        //    r.ReferansAdi = re.ReferansAdi;
        //    r.ReferansAciklama = re.ReferansAciklama;
        //    db.SaveChanges();
        //    return Redirect("/Admin/Referans/");
        //}
        //public ActionResult _ReferansSil(int RID)
        //{
        //    Referans r = db.Referans.Where(x => x.RID == RID).SingleOrDefault();
        //    db.Referans.Remove(r);
        //    db.SaveChanges();

        //    return Redirect("/Admin/Referans");
        //}
        public ActionResult MailAyar()
        {
            return View(db.MailAyar.Where(x => x.MID == 1).SingleOrDefault());
        }
        [HttpPost]
        public ActionResult MailAyar(MailAyar ma)
        {
            MailAyar m = db.MailAyar.Where(x => x.MID == 1).SingleOrDefault();
            m.GondericiMail = ma.GondericiMail;
            m.Host = ma.Host;
            m.Port = ma.Port;
            m.Sifre = ma.Sifre;
            db.SaveChanges();

            return Redirect("/Admin/MailAyar/");
        }
        public ActionResult Mesajlar()
        {
            if (Session["kullanici"] != null)
            {
                return Redirect("/Giris/Index/");
            }
            return View();
        }
        public ActionResult MesajDurum(int MID, String Durum)
        {
            Mesaj m = db.Mesaj.Where(x => x.MID == MID).SingleOrDefault();
            m.Okundu = Durum;
            db.SaveChanges();
            return Redirect("/Admin/Mesajlar");
        }
        public ActionResult _MesajSayi()
        {
            ViewBag.Sayi = db.Mesaj.Where(x => x.Okundu == "Okunmadi").Count();
            return PartialView();
        }
        public ActionResult _AdSoyad()
        {
            ViewBag.KullaniciAdi = Session["kullanici"].ToString();
            return PartialView();
        }
        public ActionResult _OkunmamisMesaj()
        {
            ViewBag.msjsayi = db.Mesaj.Where(x => x.Okundu == "Okunmadi").Count();
            return PartialView(db.Mesaj.Where(x => x.Okundu == "Okunmadi").ToList());
        }
        public ActionResult KullaniciAyar()
        {
            return View(db.Admin.Where(x => x.AID == 1).SingleOrDefault());
        }
        [HttpPost]
        public ActionResult KullaniciAyar(Admin ad)
        {
            Admin a = db.Admin.Where(x => x.AID == 1).SingleOrDefault();
            a.KulAdi = ad.KulAdi;
            a.Sifre = ad.Sifre;
            a.AdSoyad = ad.AdSoyad;
            a.Mail = ad.Mail;
            db.SaveChanges();
            return Redirect("/Admin/KullaniciAyar/");
        }
        public ActionResult _TumUrunSil()
        {
            List<Urun> u = db.Urun.ToList();
            foreach (var urun in u)
            {
                System.IO.File.Delete(Server.MapPath(urun.KapakFoto));
                List<UrunFoto> f = db.UrunFoto.Where(x => x.IlanID == urun.IlanID).ToList();
                foreach (var a in f)
                {
                    UrunFoto b = db.UrunFoto.Where(x => x.FotoID == a.FotoID).SingleOrDefault();
                    System.IO.File.Delete(Server.MapPath(b.Foto));
                    db.UrunFoto.Remove(b);
                    db.SaveChanges();
                }
                Urun ur = db.Urun.Where(x => x.IlanID ==urun.IlanID).SingleOrDefault();

                db.Urun.Remove(ur);
                db.SaveChanges();
            }

            return Redirect("/Admin/Index/");
        }
        public ActionResult _UrunResimleriSil()
        {
            List<UrunFoto> foto = db.UrunFoto.ToList();
            foreach (var ft in foto)
            {
                UrunFoto f = db.UrunFoto.Where(x => x.FotoID == ft.FotoID).SingleOrDefault();
                db.UrunFoto.Remove(f);
                db.SaveChanges();
            }
            return Redirect("/Admin/Index/");
        }
    }
}