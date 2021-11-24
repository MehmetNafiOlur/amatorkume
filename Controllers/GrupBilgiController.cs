using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlpataAmatörKüme.Controllers
{
    public class GrupBilgiController : Controller
    {
        GrupBilgiManager cm = new GrupBilgiManager(new EfGrupBilgiDal());
        // GET: GrupBilgi
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetGrupBilgi()
        {
            var grupbilgivalues = cm.GetList();
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
            // cm.GrupBilgiAddBL(p);
            GrupBilgiValidator grupBilgiValidator = new GrupBilgiValidator();
            ValidationResult results = grupBilgiValidator.Validate(p);
            if (results.IsValid)
            {
                cm.GrupBilgiAdd(p);
                return RedirectToAction("GetGrupBilgi");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }

        

        

    }
}