using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        Context c = new Context();
        // GET: CariPanel
        [Authorize]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Carilers.FirstOrDefault(x => x.CariMail == mail);
            ViewBag.m = mail;
            return View(degerler);
        }
        public ActionResult Siparislerim()
        {
            //sisteme giriş yapan mail adresini sesion olarak atadık ondan sonra  daha sonra id değişkeini tanımladık bu mail adresinin id'sini aldım sonra değerler adında değiken oluşturdum satişharekette carid ile benim aldım id değikenin listesini geriye döndürecek 
            var mail = (string)Session["CariMail"];
            var id =c.Carilers.Where(x=>x.CariMail==mail.ToString()).Select(y=>y.CariID).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);  
        }

    }
}