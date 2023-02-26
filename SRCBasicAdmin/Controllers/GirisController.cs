using SRCBasicAdmin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace SRCBasicAdmin.Controllers
{
    public class GirisController : Controller
    {
        SRCBasicDB db = new SRCBasicDB();
        // GET: Giris
        public ActionResult Index()
        {
            if (Session["kullanici"] != null)
            {
                return Redirect("/Admin/Index/");
            }
            ViewBag.Bilgi = Request.QueryString["Bilgi"];
                return View();
        }
        [HttpPost]
        public ActionResult Index(Admin a)
        {
            Admin admin = db.Admin.Where(x => x.KulAdi == a.KulAdi && x.Sifre == a.Sifre).SingleOrDefault();
            if(admin!=null)
            {
                Session["Kullanici"] = admin.AdSoyad;
                return Redirect("/Admin/Index");
            }
            else
            {
                return Redirect("/Giris/Index?Bilgi=Hatalı Kullanıcı Adı veya Şifre");
            }

        }
        public ActionResult SifreYenile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SifreYenile(Admin a)
        {
            Admin admin = db.Admin.Where(x => x.Mail == a.Mail).SingleOrDefault();
            try
            {
                MailAyar gondericimail = db.MailAyar.Where(x => x.MID == 1).SingleOrDefault();
                WebMail.SmtpServer = gondericimail.Host.ToString(); ;
                WebMail.EnableSsl = true;
                WebMail.UserName = gondericimail.GondericiMail;
                WebMail.Password = gondericimail.Sifre; // gerçek dışı
                WebMail.SmtpPort = Convert.ToInt32(gondericimail.Port);
                WebMail.Send(admin.Mail, "S R C ADMİN ŞİFRE HATIRLATICI", "<p>Merhabalar " + admin.AdSoyad + " Kullanıcı Bilgilerinizi Aşağıda Size Sunuyorum Okuduktan Sonra Maili Silmenizi Öneririm.<br /><br /><br />Kullanıcı Adı:" + admin.KulAdi + "<br />Şifre:" + admin.Sifre + "<br /><br /> Saygılarımla S R C Admin", gondericimail.GondericiMail);

            }
            catch 
            {

            
            }
            return Redirect("/Giris/Index?Bilgi=Sifreniz Mailinize Gönderildi");
        }
        public ActionResult CikisYap()
        {
            Session.Clear();
            Session.Abandon();
            return Redirect("/Giris/Index/");
        }
       
    }
}