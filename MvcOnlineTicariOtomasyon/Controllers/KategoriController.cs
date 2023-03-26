using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        // GET: Kategori
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Kategoris.ToList();
            return View(degerler);
        }

        [HttpGet] // sayfa yenilendinde view görüntülendinde boş olarak
        public ActionResult KategoriEkle()
        {
            return View();
        }

        [HttpPost] // butona tıkladıgı zamn 
        public ActionResult KategoriEkle(Kategori k)
        {
            c.Kategoris.Add(k);
            c.SaveChanges(); // klasik adonet karşılı  ExeCuteNonQuery  unutma aklıma geldi :))

            return RedirectToAction("Index");
        }
       
        public ActionResult KategoriSil(int id)
        {
            var ktg = c.Kategoris.Find(id);
            c.Kategoris.Remove(ktg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult KategoriGetir(int id)
        {
            var kategori = c.Kategoris.Find(id);
            return View("KategoriGetir",kategori);
        }

        public ActionResult KategoriGuncelle(Kategori k)
        {
            var kate=c.Kategoris.Find(k.KtegoriID);
            kate.KategoriAd = k.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}