using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index()
        {
            var urunler = c.Uruns.ToList();
            return View(urunler);
        }

        [HttpGet]
        public ActionResult UrunEkle()
        {
            //// bu işlem sayfa yüklendi zamn gelsin
            //List<SelectListItem> kategorilist = (from x in c.Kategoris.ToList()
            //                                     select new SelectListItem
            //                                     {
            //                                         Text = x.KategoriAd,
            //                                         Value = x.KategoriAd.ToString()
            //                                     }).ToList();
            //ViewBag.k1 = kategorilist;
            return View();
        }

        [HttpPost]
        public ActionResult UrunEkle(Urun U)
        {
            c.Uruns.Add(U);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}