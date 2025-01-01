using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class UrunlerController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult UserIndex()
        {
            int ktg = Convert.ToInt32(Request.QueryString["UrunKategori"]);
            var degerler = db.Tbl_Urunler.Where(x => x.UrunStok > 0 && x.Durum == true && x.UrunKategori == ktg).ToList();
            return View(degerler);
        }
        public ActionResult Index()
        {
            int ktg = Convert.ToInt32(Request.QueryString["UrunKategori"]);
            var degerler = db.Tbl_Urunler.Where(x => x.Durum == true && x.UrunKategori == ktg).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> ktg = db.Tbl_Kategori.
                Select(x => new SelectListItem
                {
                    Text = x.KategoriAd,
                    Value = x.KategoriId.ToString()
                }).ToList();
            ViewBag.drop = ktg;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Tbl_Urunler p)
        {
            p.Durum = true;
            var ktgr = db.Tbl_Kategori.Where(x => x.KategoriId == p.Tbl_Kategori.KategoriId).FirstOrDefault();
            p.Tbl_Kategori = ktgr;
            db.Tbl_Urunler.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index", "Urunler", new { UrunKategori = p.UrunKategori });
        }
        public ActionResult UrunSil(int id)
        {
            var urun = db.Tbl_Urunler.Find(id);
            urun.Durum = false;
            db.SaveChanges();
            return RedirectToAction("Index", "Urunler", new { UrunKategori = urun.UrunKategori });
        }

    }
}