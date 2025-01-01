using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class SatislarController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            var degerler = db.Tbl_Satislar.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult SatinAl(int UrunId)
        {

            var musteriTelefon = Session["MusteriTelefon"].ToString();
            var urun = db.Tbl_Urunler.Find(UrunId);
            var musteri = db.Tbl_Musteri.Find(musteriTelefon);

            ViewBag.Urun = urun.UrunMarka + " " + urun.UrunAd;
            ViewBag.Musteri = musteri.MusteriAd + " " + musteri.MusteriSoyad;


            return View();
        }
        [HttpPost]
        public ActionResult SatinAl(Tbl_Satislar sts, int UrunId)
        {
            sts.Urun = UInt16.Parse(Request.QueryString["UrunId"]);
            sts.Musteri = Session["MusteriTelefon"].ToString();
            db.Tbl_Satislar.Add(sts);
			Tbl_Urunler urun = db.Tbl_Urunler.Find(UrunId);
			urun.UrunStok = Convert.ToByte(urun.UrunStok - sts.Adet);
            sts.Tarih = DateTime.Parse(DateTime.Now.ToString());
			db.SaveChanges();

            return RedirectToAction("Index", "Alimlar");
        }
    }
}