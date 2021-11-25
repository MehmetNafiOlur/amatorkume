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
    public class AdminDenemeController : Controller
    {
        PuanBilgiManager pb = new PuanBilgiManager(new EfPuanBilgiDal());
        GrupBilgiManager gb = new GrupBilgiManager(new EfGrupBilgiDal());
        TakimBilgiManager cm = new TakimBilgiManager(new EfTakimBilgiDal());

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetTakimBilgiAd()
        {
          
            var takimbilgivalues = cm.GetList();
            return View(takimbilgivalues);
        }

        [HttpGet]
        public ActionResult AddTakimBilgiAd()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult AddTakimBilgiAd(TakimBilgi p, HttpPostedFileBase TakimLogo)
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

            return RedirectToAction("GetTakimBilgiAd");
        }


    }

    
}