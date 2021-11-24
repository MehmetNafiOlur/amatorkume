using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
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
        PuanBilgiManager pb = new PuanBilgiManager(new EfPuanBilgiDal());
        GrupBilgiManager gb = new GrupBilgiManager(new EfGrupBilgiDal());
        TakimBilgiManager cm = new TakimBilgiManager(new EfTakimBilgiDal());
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTakimBilgi()
        {
            ViewBag.GrupBilgi = gb.GetList();
            var takimbilgivalues = cm.GetList();
            return View(takimbilgivalues);
        }

        [HttpGet]
        public ActionResult AddTakimBilgi()
        {
            var grupBilgis = gb.GetList();
            return View(grupBilgis);

        }


        [HttpPost]
        public ActionResult AddTakimBilgi(TakimBilgi p, HttpPostedFileBase TakimLogo)
        {
            if (TakimLogo != null)
            {
                string extension = System.IO.Path.GetExtension(TakimLogo.FileName);
                Random rnd = new Random();
                int random = rnd.Next(10000, 99999);
                string Path = @"Image\" + random.ToString() + "-" + TakimLogo.FileName;
                for (; ; )
                {
                    if (System.IO.File.Exists(Server.MapPath("~") + Path))
                    {
                        random = rnd.Next(10000, 99999);
                        Path = @"Image\" + random.ToString() + "-" + TakimLogo.FileName;
                    }
                    else
                    {
                        break;
                    }
                }

                Path = @"Image\" + random.ToString() + "-" + TakimLogo.FileName;

                TakimLogo.SaveAs(Server.MapPath("~") + Path);

                p.TakimLogo = "\\" + Path;
                cm.TakimBilgiAdd(p);
            }
   
            return RedirectToAction("GetTakimBilgi");
        }

        [HttpGet]
        public JsonResult Grupcek(int grupid)
        {
            //hgfhgjfh

            var takimlar = cm.GetList().Where(i => i.Grupid == grupid).ToList();
   
            foreach (var item in takimlar)
            {
                var puanlar = pb.GetList().Where(x => x.Puanid == item.Puanid).ToList();

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
            ViewBag.GrupList = gb.GetList();
            var takimbilgivalues = cm.GetList();
            return View(takimbilgivalues);
        }

        [HttpPost]
        public ActionResult GrupAta(TakimBilgi p)
        {
            ViewBag.GrupList = gb.GetList();
            cm.TakimBilgiAdd(p);
            return RedirectToAction("AddTakimBilgi");
        }

   
        public ActionResult GrupAtaKaydet(int Id, int GrupId)
        {
            var takim = cm.GetList().Where(i => i.Takimid == Id).FirstOrDefault();

            if (takim != null)
            {
                takim.Grupid = GrupId;

                cm.TakimBilgiAdd(takim);

                return Json("Başarılı", JsonRequestBehavior.AllowGet);
            }

            return Json("Hata", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult PuanAta()
        {
            ViewBag.PuanList = pb.GetList();
            var takimbilgivalues = cm.GetList();
            return View(takimbilgivalues);
        }

        [HttpPost]
        public ActionResult PuanAta(TakimBilgi p)
        {
            ViewBag.PuanList = pb.GetList();
            cm.TakimBilgiAdd(p);
            return RedirectToAction("AddTakimBilgi");
        }

        public ActionResult PuanAtaKaydet(int Id, int PuanId)
        {
            var takim = cm.GetList().Where(i => i.Takimid == Id).FirstOrDefault();

            if (takim != null)
            {
                takim.Puanid = PuanId;
                cm.TakimBilgiAdd(takim);
                return Json("Başarılı", JsonRequestBehavior.AllowGet);
            } else
            {
                return Json("Hata", JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpGet]
        public ActionResult PuanGüncelleme()
        {
            ViewBag.PuanList = pb.GetList();
            var takimbilgivalues = cm.GetList();
            return View(takimbilgivalues);
        }

    

    }
}