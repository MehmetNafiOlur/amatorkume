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
    public class HakemController : Controller
    {
      
        HakemManager hm = new HakemManager(new EfHakemDal());
        
        // GET: Hakem
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddHakem()
        {
            var q = hm.GetList();
            return View();
        
        }

        public ActionResult AddHakem(Hakem p)
        {
            hm.HakemAdd(p);
            return RedirectToAction("AddHakem");  

        }

    }
}