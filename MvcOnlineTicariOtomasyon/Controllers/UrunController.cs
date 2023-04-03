using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.WebPages.Html;
using MvcOnlineTicariOtomasyon.Models.Siniflar;
using SelectListItem = System.Web.Mvc.SelectListItem;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index()
        {
            var urunler = c.Uruns.Where(x=>x.Durum==true).ToList();
            return View(urunler);
        }

        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KtegoriID.ToString()

                                           }).ToList();
            ViewBag.dgr1 = deger1;
           return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(Urun U)
        {
            c.Uruns.Add(U);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunSil(int id)
        {
            var deger = c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KtegoriID.ToString()

                                           }).ToList();
            ViewBag.dgr1 = deger1;

            var urun = c.Uruns.Find(id);
            return View("UrunGetir", urun);
        }

        public ActionResult UrunGuncelle(Urun u)
        {
            var uru = c.Uruns.Find(u.UrunID);
            uru.UrunAd = u.UrunAd;
            uru.AlisFiyati = u.AlisFiyati;
            uru.SatisFiyati=u.SatisFiyati;
            uru.Durum = u.Durum;
            uru.Stok = u.Stok;
            uru.Marka = u.Marka;
            uru.Kategoriid = u.Kategoriid;
            uru.UrunGorsel = u.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunListesi()
        {
            var degerler = c.Uruns.ToList();
            return View(degerler);
        }

    }
}