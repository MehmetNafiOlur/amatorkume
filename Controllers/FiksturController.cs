using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlpataAmatörKüme.Controllers
{
    public class FiksturController : Controller
    {
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