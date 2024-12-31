using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class AlimlarController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index()
        {
            string user = Session["MusteriTelefon"].ToString();
            var degerler = db.Tbl_Satislar.Where(x=>x.Musteri==user).ToList();
            return View(degerler);
        }
    }
}