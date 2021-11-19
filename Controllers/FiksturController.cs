using BusinessLayer.Concrete;
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

        public ActionResult FiksturEkle()
        {
            return View();
        }
    }
}