using SRCBasicAdmin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace SRCBasicAdmin.Controllers
{
    public class HomeController : Controller
    {
        SRCBasicDB db = new SRCBasicDB();

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Hakkimizda()
        {
            return View();
        }
        public ActionResult Ilanlar(int s=1)
        {
            int listeurunsayi = 9;
            int sayfasayi = 0;
            int ilansayi = db.Urun.Count();
            if (ilansayi%listeurunsayi>0)
            {
                sayfasayi = (ilansayi / listeurunsayi)+1;
            }
            else
            {
                sayfasayi = (ilansayi / listeurunsayi);
            }
            var ilanlar = db.Urun.ToList().OrderByDescending(x=> x.IlanID).ToPagedList(s, listeurunsayi);
            ViewBag.SayfaSayi = sayfasayi;
            ViewBag.xSayfa = s;
            return View(ilanlar);
        }
        public ActionResult SatilikIlanlar(int s = 1)
        {
            int listeurunsayi = 9;
            int sayfasayi = 0;
            int ilansayi = db.Urun.Count();
            if (ilansayi % listeurunsayi > 0)
            {
                sayfasayi = (ilansayi / listeurunsayi) + 1;
            }
            else
            {
                sayfasayi = (ilansayi / listeurunsayi);
            }
            var ilanlar = db.Urun.Where(x=> x.IlanTip=="Satılık").ToList().OrderByDescending(x => x.IlanID).ToPagedList(s, listeurunsayi);
            ViewBag.SayfaSayi = sayfasayi;
            ViewBag.xSayfa = s;
            return View(ilanlar);
        }
        public ActionResult KiralikIlanlar(int s = 1)
        {
            int listeurunsayi = 9;
            int sayfasayi = 0;
            int ilansayi = db.Urun.Count();
            if (ilansayi % listeurunsayi > 0)
            {
                sayfasayi = (ilansayi / listeurunsayi) + 1;
            }
            else
            {
                sayfasayi = (ilansayi / listeurunsayi);
            }
            var ilanlar = db.Urun.Where(x => x.IlanTip == "Kiralık").ToList().OrderByDescending(x => x.IlanID).ToPagedList(s, listeurunsayi);
            ViewBag.SayfaSayi = sayfasayi;
            ViewBag.xSayfa = s;
            return View(ilanlar);
        }
        public ActionResult IlanDetay(int IlanID)
        {
            return View(db.Urun.Where(x=> x.IlanID==IlanID).SingleOrDefault());
        }
        public ActionResult Iletisim()
        {
            return View();
        }
        public ActionResult _MesajGonder(Mesaj me)
        {
            if (me.Mesaj1 != null)
            {

                Mesaj m = new Mesaj();
                m.Ad = me.Ad;
                m.Mail = me.Mail;
                m.Mesaj1 = me.Mesaj1;
                m.Konu = me.Konu;
                m.Okundu = "Okunmadi";
                m.Tarih = DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString();
                db.Mesaj.Add(m);
                db.SaveChanges();

                Admin admin = db.Admin.Where(x => x.AID == 1).SingleOrDefault();
                try
                {
                    MailAyar gondericimail = db.MailAyar.Where(x => x.MID == 1).SingleOrDefault();
                    WebMail.SmtpServer = gondericimail.Host.ToString();
                    WebMail.EnableSsl = true;
                    WebMail.UserName = gondericimail.GondericiMail;
                    WebMail.Password = gondericimail.Sifre; // gerçek dışı
                    WebMail.SmtpPort = Convert.ToInt32(gondericimail.Port);
                    WebMail.Send(admin.Mail, "S R C ADMİN MESAJINIZ VAR", "<p>Merhabalar " + admin.AdSoyad + "<br/><br/> Websitenizden Bir Mesajınız Var <br /><br /> Gönderilen Tarih:" + DateTime.Now.ToShortDateString() + "-" + DateTime.Now.ToShortTimeString() + "<br /> Gönderen İsim Soyisim:" + me.Ad + "  <br /> Gönderen Mail:" + me.Mail + "<br /> Mesajın Konusu:" + me.Konu + "<br /> Mesaj:" + me.Mesaj1 + "<br /><br /> Saygılarımla S R C Admin", gondericimail.GondericiMail);


                    WebMail.SmtpServer = gondericimail.Host.ToString(); ;
                    WebMail.EnableSsl = true;
                    WebMail.UserName = gondericimail.GondericiMail;
                    WebMail.Password = gondericimail.Sifre; // gerçek dışı
                    WebMail.SmtpPort = Convert.ToInt32(gondericimail.Port);
                    WebMail.Send(me.Mail, "Mesajınızı Aldık | Yagmur Metal", "<p>Merhabalar " + me.Ad + "<br/><br/> Mesajınızı Aldık En Kısa Sürede Sizinle İletişime Geçecegiz <br /><br/><br />Saygılarımızla YAGMUR METAL İletişim Ekibi.", gondericimail.GondericiMail);
                    return Redirect("/Home/Index?Mesaj=Mesajınızı Aldık En Kısa Sürede İletişime Geçecegiz");
                }
                catch (Exception)
                {


                }

            }
            return Redirect("/Home/FormMesaj?adSoyad="+me.Ad);
        }
        public ActionResult FormMesaj(string adSoyad)
        {
            ViewBag.msjadsoyad = adSoyad;
            return View();
        }
    }
}