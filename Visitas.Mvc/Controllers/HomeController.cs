using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visitas.UnitOfWork;

namespace Visitas.Mvc.Controllers
{
    public class HomeController: BaseController
    {
        public HomeController(ILog log, IUnitOfWork unit) : base(log, unit)
        {
            //_unit = new NorthwindUnitOfWork(ConfigurationManager.ConnectionStrings["NorthwindConnection"].ToString());
            //_unit = unit;
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            _log.Info("Prueba del Log");
            return View();             
        }
           
        public ActionResult Objetivos()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}