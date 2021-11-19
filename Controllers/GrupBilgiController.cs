using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlpataAmatörKüme.Controllers
{
    public class GrupBilgiController : Controller
    {
        GrupBilgiManager cm = new GrupBilgiManager();
        // GET: GrupBilgi
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetGrupBilgi()
        {
            var grupbilgivalues = cm.GetAllBL();
            return View(grupbilgivalues);
        }

        [HttpGet]
        public ActionResult AddGrupBilgi()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddGrupBilgi(GrupBilgi p)
        {
            cm.GrupBilgiAddBL(p);
            return RedirectToAction("GetGrupBilgi");
        }

        

        

    }
}