using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlpataAmatörKüme.Controllers
{
    public class TakimBilgiController : Controller
    {
        PuanBilgiManager pb = new PuanBilgiManager();
        GrupBilgiManager gb = new GrupBilgiManager();
        TakimBilgiManager cm = new TakimBilgiManager();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTakimBilgi()
        {
            ViewBag.GrupBilgi = gb.GetAllBL();
            var takimbilgivalues = cm.GetAllBL();
            return View(takimbilgivalues);
        }

        [HttpGet]
        public ActionResult AddTakimBilgi()
        {
            var grupBilgis = gb.GetAllBL();
            return View(grupBilgis);

        }


        [HttpPost]
        public ActionResult AddTakimBilgi(TakimBilgi p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~Image" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.TakimLogo = "Image" + dosyaadi + uzanti;
            }
            cm.TakimBilgiAddBL(p);
          
            return RedirectToAction("GetTakimBilgi");
        }

        [HttpGet]
        public JsonResult Grupcek(int grupid)
        {
            //hgfhgjfh

            var takimlar = cm.GetAllBL().Where(i => i.Grupid == grupid).ToList();
   
            foreach (var item in takimlar)
            {
                var puanlar = pb.GetAllBL().Where(x => x.Puanid == item.Puanid).ToList();

                if (puanlar != null)
                {
                    item.PuanBilgis = puanlar;
                }
            }

            var takimlar2 = takimlar.OrderByDescending(x => x.PuanBilgis.FirstOrDefault().Puan).OrderByDescending(x => x.PuanBilgis.FirstOrDefault().Averaj);

            return Json(new { data = takimlar2 }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult GrupAta()
        {
            ViewBag.GrupList = gb.GetAllBL();
            var takimbilgivalues = cm.GetAllBL();
            return View(takimbilgivalues);
        }

        [HttpPost]
        public ActionResult GrupAta(TakimBilgi p)
        {
            ViewBag.GrupList = gb.GetAllBL();
            cm.TakimBilgiAddBL(p);
            return RedirectToAction("AddTakimBilgi");
        }

   
        public ActionResult GrupAtaKaydet(int Id, int GrupId)
        {
            var takim = cm.GetAllBL().Where(i => i.Takimid == Id).FirstOrDefault();

            if (takim != null)
            {
                takim.Grupid = GrupId;

                cm.TakimBilgiUpdateBL(takim);

                return Json("Başarılı", JsonRequestBehavior.AllowGet);
            }

            return Json("Hata", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PuanAta()
        {
            ViewBag.PuanList = pb.GetAllBL();
            var takimbilgivalues = cm.GetAllBL();
            return View(takimbilgivalues);
        }

        [HttpPost]
        public ActionResult PuanAta(TakimBilgi p)
        {
            ViewBag.PuanList = pb.GetAllBL();
            cm.TakimBilgiAddBL(p);
            return RedirectToAction("AddTakimBilgi");
        }

        public ActionResult PuanAtaKaydet(int Id, int PuanId)
        {
            var takim = cm.GetAllBL().Where(i => i.Takimid == Id).FirstOrDefault();

            if (takim != null)
            {
                takim.Puanid = PuanId;
                cm.TakimBilgiUpdateBL(takim);
                return Json("Başarılı", JsonRequestBehavior.AllowGet);
            } else
            {
                return Json("Hata", JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpGet]
        public ActionResult PuanGüncelleme()
        {
            ViewBag.PuanList = pb.GetAllBL();
            var takimbilgivalues = cm.GetAllBL();
            return View(takimbilgivalues);
        }

    

    }
}