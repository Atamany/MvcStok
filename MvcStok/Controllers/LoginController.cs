using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcStok.Controllers
{
    public class LoginController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        [HttpGet]
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(Tbl_Musteri t)
        {
            var bilgiler = db.Tbl_Musteri.FirstOrDefault(x => x.MusteriTelefon == t.MusteriTelefon && x.MusteriSifre == t.MusteriSifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.MusteriTelefon.ToString(), false);
                Session["MusteriTelefon"] = bilgiler.MusteriTelefon.ToString();
                return RedirectToAction("UserMusteri", "UserMusteri");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult KayitOl()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KayitOl(string MusteriTelefon, string MusteriAd, string MusteriSoyad, string password1, string password2)
        {
            if(password1 == password2)
            {
                Tbl_Musteri mstr = new Tbl_Musteri();
                mstr.Durum = true;
                mstr.MusteriTelefon = MusteriTelefon;
                mstr.MusteriAd = MusteriAd;
                mstr.MusteriSoyad = MusteriSoyad;
                mstr.MusteriSifre = password1;
                db.Tbl_Musteri.Add(mstr);
                db.SaveChanges();
                return RedirectToAction("Giris");
            }
            else {
                return View();
            }
        }
    }
}