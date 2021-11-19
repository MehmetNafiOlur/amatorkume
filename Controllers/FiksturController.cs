using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlpataAmatörKüme.Controllers
{
    public class FiksturController : Controller
    {  
        FiksturManager fm = new FiksturManager();
        // GET: Fikstur
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FiksturEkle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FiksturEkle(Fikstur p)
        {
            fm.FiksturAddBL(p);
            return RedirectToAction("FiksturEkle");
        }
    }
}