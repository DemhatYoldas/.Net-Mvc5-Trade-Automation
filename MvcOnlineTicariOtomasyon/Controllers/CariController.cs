﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Carilers.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult CariEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CariEkle(Cariler d)
        {
            d.Durum = true;
            c.Carilers.Add(d);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariSil(int id)
        {
            var cr = c.Carilers.Find(id);
            cr.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CariGetir(int id)
        {
            var cari = c.Carilers.Find(id);
            // carigetir view ile cari değişkenini döndürüyoruz
            return View("CariGetir", cari);
        }

        public ActionResult CariGuncelle(Cariler p)
        {
            //modelState kastı sınıf içersinde zorunlu kıldımız alanlar 
            if (!ModelState.IsValid)// modelstate geçerlemesi doğrudeğilse carigetir geri döndürür
            {
                return View("CariGetir");
            }
            else // model state geçerli ise  işlemler oluur 
            {
                var cari = c.Carilers.Find(p.CariID);
                cari.CariAd = p.CariAd;
                cari.CariSoyad = p.CariSoyad;
                cari.CariSehir = p.CariSehir;
                cari.CariMail = p.CariMail;
                c.SaveChanges();
                return RedirectToAction("Index");
            }  
        }

        public ActionResult MusteriSatis(int id)
        {
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            var cr = c.Carilers.Where(x => x.CariID == id).Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cari = cr;
            return View(degerler);
        }
    }
}