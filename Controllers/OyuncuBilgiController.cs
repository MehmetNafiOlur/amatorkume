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
    public class OyuncuBilgiController : Controller
    {

        OyuncuBilgiManager ob = new OyuncuBilgiManager(new EfOyuncuBilgiDal());
        // GET: OyuncuBilgi
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddOyuncuBilgi()
        {
            var q = ob.GetList();
            return View();
        }

        [HttpPost]
        public ActionResult AddOyuncuBilgi(OyuncuBilgi p)
        {
            ob.OyuncuBilgiAdd(p);
            return RedirectToAction("AddOyuncuBilgi");
           
        }
    }
}