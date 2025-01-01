using MvcStok.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
			var musteribul = db.Tbl_Musteri.FirstOrDefault(m => m.MusteriTelefon == user);
			if (musteribul.Durum == false)
			{
				ViewBag.MusteriDurum = false;
				ViewBag.HesapDurumu = "Pasif";
			}
			else if (musteribul.Durum == true)
			{
				ViewBag.MusteriDurum = true;
				ViewBag.HesapDurumu = "Aktif";
			}
			return View(degerler);
		}
		public ActionResult MusteriSil()
		{
			var musteribul = Session["MusteriTelefon"].ToString();
			Tbl_Musteri mstr = db.Tbl_Musteri.Find(musteribul);
			mstr.Durum = false;
			db.SaveChanges();
			return RedirectToAction("UserMusteri");
		}
		public ActionResult MusteriAktive()
		{
			var musteribul = Session["MusteriTelefon"].ToString();
			Tbl_Musteri mstr = db.Tbl_Musteri.Find(musteribul);
			mstr.Durum = true;
			db.SaveChanges();
			return RedirectToAction("userMusteri");
		}
		[HttpGet]
		public ActionResult SifreGuncelle()
		{
			return View();
		}
		[HttpPost]
		public ActionResult SifreGuncelle(string password1, string password2)
		{

			if (password1 == password2)
			{
				Tbl_Musteri mstr = db.Tbl_Musteri.Find(Session["MusteriTelefon"]);
				mstr.MusteriSifre = password1;
				db.SaveChanges();
				return RedirectToAction("userMusteri");
			}
			else
			{
				return View();
			}
		}

	}
}