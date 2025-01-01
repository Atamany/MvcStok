using MvcStok.Models.Entity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            var degerler = db.Tbl_Musteri.Where(x => x.Durum == true).ToList().ToPagedList(sayfa, 10);
            return View(degerler);
        }
    }
}