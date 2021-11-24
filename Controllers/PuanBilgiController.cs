using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlpataAmatörKüme.Controllers
{
    public class PuanBilgiController : Controller
    {
        PuanBilgiManager pb = new PuanBilgiManager(new EfPuanBilgiDal());
        TakimBilgiManager cm = new TakimBilgiManager(new EfTakimBilgiDal());

        // GET: PuanBilgi
        public ActionResult Index()
        {
            return View();  
        }

    
        public ActionResult GetPuanBilgi()
        {
            var puanBilgis = pb.GetList();
            ViewBag.TakimList = cm.GetList();
            return View(puanBilgis);
        }


        [HttpGet]
        public ActionResult AddPuanBilgi()
        {
            var q = cm.GetList();
            return View(q);
        }


        [HttpPost]
        public ActionResult AddPuanBilgi(PuanBilgi p)
        {
            pb.PuanBilgiAdd(p);
            return RedirectToAction("GetPuanBilgi");
        }
       
        public ActionResult PuanAta()
        {
            ViewBag.TakimList = cm.GetList();
            var takimBilgis = cm.GetList();  
            return View();
        }


        [HttpGet]
        public ActionResult PuanGuncelle()
        {
            ViewBag.TakimList = cm.GetList();
            var puanbilgivalues = pb.GetList();
            return View(puanbilgivalues);
        }


        public ActionResult PuanGuncellemekaydet(int? puanId, string oynananOyun,string galibiyet,string beraberlik,string malubiyet,string atilanGol,string yenilenGol,string averaj,string puan)
        {
            var up = pb.GetList().Where(x => x.Puanid == puanId).FirstOrDefault();

            up.OynananOyun = oynananOyun;
            up.Galibiyet = galibiyet;
            up.Beraberlik = beraberlik;
            up.Malubiyet = malubiyet;
            up.AtilanGol = atilanGol;
            up.YenilenGol = yenilenGol;
            up.Averaj = averaj;
            up.Puan = puan;

            pb.PuanBilgiAdd(up);

               
            return Json("Başarılı", JsonRequestBehavior.AllowGet);
        }


    }
}
          