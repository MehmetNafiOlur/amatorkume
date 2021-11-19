using BusinessLayer.Concrete;
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
        PuanBilgiManager pb = new PuanBilgiManager();
        TakimBilgiManager cm = new TakimBilgiManager();

        // GET: PuanBilgi
        public ActionResult Index()
        {
            return View();  
        }

    
        public ActionResult GetPuanBilgi()
        {
            var puanBilgis = pb.GetAllBL();
            ViewBag.TakimList = cm.GetAllBL();
            return View(puanBilgis);
        }


        [HttpGet]
        public ActionResult AddPuanBilgi()
        {
            var q = cm.GetAllBL();
            return View(q);
        }


        [HttpPost]
        public ActionResult AddPuanBilgi(PuanBilgi p)
        {
            pb.PuanBilgiAddBL(p);
            return RedirectToAction("GetPuanBilgi");
        }
       
        public ActionResult PuanAta()
        {
            ViewBag.TakimList = cm.GetAllBL();
            var takimBilgis = cm.GetAllBL();  
            return View();
        }


        [HttpGet]
        public ActionResult PuanGuncelle()
        {
            ViewBag.TakimList = cm.GetAllBL();
            var puanbilgivalues = pb.GetAllBL();
            return View(puanbilgivalues);
        }


        public ActionResult PuanGuncellemekaydet(int? puanId, string oynananOyun,string galibiyet,string beraberlik,string malubiyet,string atilanGol,string yenilenGol,string averaj,string puan)
        {
            var up = pb.GetAllBL().Where(x => x.Puanid == puanId).FirstOrDefault();

            up.OynananOyun = oynananOyun;
            up.Galibiyet = galibiyet;
            up.Beraberlik = beraberlik;
            up.Malubiyet = malubiyet;
            up.AtilanGol = atilanGol;
            up.YenilenGol = yenilenGol;
            up.Averaj = averaj;
            up.Puan = puan;

            pb.PuanBilgiUpdateBL(up);

               
            return Json("Başarılı", JsonRequestBehavior.AllowGet);
        }


    }
}
          