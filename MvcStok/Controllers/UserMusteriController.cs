using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok.Controllers
{
    public class UserMusteriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult UserMusteri()
        {
            string user = Session["MusteriTelefon"].ToString();
            var degerler = db.Tbl_Musteri.Where(x => x.MusteriTelefon == user).ToList();
            return View(degerler);
        }
        
    }
}