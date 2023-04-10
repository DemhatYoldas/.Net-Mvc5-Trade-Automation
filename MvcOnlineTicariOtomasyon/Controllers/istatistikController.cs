using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class istatistikController : Controller
    {
        // GET: istatistik
        Context c = new Context();
        public ActionResult Index()
        {
            var deger1 = c.Carilers.Count().ToString();
            ViewBag.d1 = deger1;

            var deger2 = c.Uruns.Count().ToString();
            ViewBag.d2 = deger2;

            var deger3 = c.Personels.Count().ToString();
            ViewBag.d3 = deger3;

            var deger4 = c.Kategoris.Count().ToString();
            ViewBag.d4 = deger4;

            // toplam stok sayısı
            var deger5 = c.Uruns.Sum(x => x.Stok).ToString();
            ViewBag.d5 = deger5;

            // 20 altında olan ürün sayısı kritik seviye 
            var deger7 = c.Uruns.Count(x => x.Stok <= 20).ToString();
            ViewBag.d7 = deger7;

            //ürünler içersinde markayı seç seçmiş oldun markayı tekrarsız olarak say
            // Distinct tekrarsız , count say demek , ve to string yanı string olarak yazdır 
            var deger6 = (from x in c.Uruns select x.Marka).Distinct().Count().ToString();
            ViewBag.d6 = deger6;

            // sıraladık sonra bu listedeki en yüksek değerin adını getirecek
            var deger8 = (from x in c.Uruns orderby x.SatisFiyati descending select x.UrunAd).FirstOrDefault();
            ViewBag.d8 = deger8;

            // buseferde a'dan z'ye sıraladık en küçük ürünü bulacak
            // ascending = a'dan z' sıralama
            var deger9 = (from x in c.Uruns orderby x.SatisFiyati ascending select x.UrunAd).FirstOrDefault();
            ViewBag.d9 = deger9;

            var deger10 = c.Uruns.Count(x => x.UrunAd == "Buzdolabı").ToString();
            ViewBag.d10 = deger10;

            var deger11 = c.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
            ViewBag.d11 = deger11;

            var deger14 = c.SatisHarekets.Sum(x => x.ToplamTutar).ToString();
            ViewBag.d14 = deger14;

            // iç sorgu en çok olan ürün id veriyor dış sorguda onunla eşitlenip adını yazdırıyor 
            var deger13 = c.Uruns.Where(u => u.UrunID == (c.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault())).Select(k => k.UrunAd).FirstOrDefault();
            ViewBag.d13 = deger13;

            //var deger13 = c.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            //ViewBag.d13 = deger13;

            // OrderByDescending sıraladında marka değeri en yüksek ilk başta olucak marka değerini seçip getirecek
            var deger12 = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.d12 = deger12;

            DateTime bugun = DateTime.Today;
            var deger15 = c.SatisHarekets.Count(x => x.Tarih == bugun).ToString();
            ViewBag.d15 = deger15;


            //var deger16 = c.SatisHarekets.Where(x => x.Tarih == bugun).Sum(y => y.ToplamTutar).ToString();
            //ViewBag.d16 = deger16;

            return View();
        }

        public ActionResult KolayTablolar()
        {
            var sorgu = from x in c.Carilers
                        group x by x.CariSehir into g
                        select new SinifGrup
                        {
                            // grubun içersine dahil olacak alan
                            Sehir = g.Key, 
                            // grubumun iligili sayıca ifadesi
                            Sayi = g.Count()
                        };
            return View(sorgu.ToList());
        }

        // Partial view de layout gibi
        // ama layout tamamını kapsar
        // Partial view ise sadece belirli kısmı kapsar 

        public PartialViewResult Partial1()
        {
            var sorgu2 = from x in c.Personels
                         group x by x.Departman.DepartmanAd into g
                         select new SinifGrup2
                         {
                             Deparman = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu2.ToList());
        }

        public PartialViewResult Partial2()
        {
            var sorgu3=c.Carilers.ToList();
            return PartialView(sorgu3);
        }

        public PartialViewResult Partial3()
        {
            var sorgu4=c.Uruns.ToList();
            return PartialView(sorgu4);
        }

        public PartialViewResult partial4()
        {
            var sorgu4 = from x in c.Uruns
                         group x by x.Marka into g
                         select new SinifGrup3
                         {
                             marka = g.Key,
                             sayi = g.Count()
                         };
            return PartialView(sorgu4.ToList());
        }
    }
}