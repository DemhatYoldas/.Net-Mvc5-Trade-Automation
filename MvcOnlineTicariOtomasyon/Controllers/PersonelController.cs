using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler=c.Personels.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.DepartmanID.ToString()

                                           }).ToList();
            ViewBag.dgr1 = deger1;
            return View();
        }

        [HttpPost]
        public ActionResult PersonelEkle(Personel d)
        {
            c.Personels.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelSil(int id)
        {
            var dpr = c.Personels.Find(id);
            dpr.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.DepartmanAd,
                                               Value = x.DepartmanID.ToString()

                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var personel = c.Personels.Find(id);
            return View("PersonelGetir", personel);
        }

        public ActionResult PersonelGuncelle(Personel d)
        {
            var per = c.Personels.Find(d.PersonelID);
            per.PersonelAd = d.PersonelAd;
            per.PersonelSoyad = d.PersonelSoyad;
            per.PersonelGorsel = d.PersonelGorsel;
            per.Departmanid = d.Departmanid;
            per.Durum=d.Durum;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}