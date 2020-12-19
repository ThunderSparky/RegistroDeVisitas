using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visitas.Models;
using Visitas.Mvc.ExternalServices;
using Visitas.UnitOfWork;

namespace Visitas.Mvc.Controllers
{
    [RoutePrefix("Visitante")]
    public class VisitanteController : BaseController
    {
        public VisitanteController(ILog log, IUnitOfWork unit, IExternalAPIToken externalAPIToken) : base(log, unit, externalAPIToken)
        {

        }
        public ActionResult Index()
        {
            return View(_unit.Visitantes.GetList());
        }
        public PartialViewResult Create()
        {
            ViewBag.Instituto = _unit.Institutos.GetList(); //Para traer la lista de los institutos
            return PartialView("_Create", new Visitantes());
        }
        [HttpPost]
        public ActionResult Create(Visitantes visitante)
        {
            if (ModelState.IsValid)
            {
                _unit.Visitantes.Insert(visitante);
                return RedirectToAction("Index");
            }
            return PartialView("_Create", visitante);
        }
        public PartialViewResult Edit(int id)
        {
            ViewBag.Instituto = _unit.Institutos.GetList(); //Para traer la lista de los institutos
            return PartialView("_Edit", _unit.Visitantes.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(Visitantes visitante)
        {
            if (_unit.Visitantes.Update(visitante)) return RedirectToAction("Index");

            return PartialView("_Edit", visitante);
        }
        public PartialViewResult Delete(int id)
        {
            return PartialView("_Delete", _unit.Visitantes.GetById(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(Visitantes visitante)
        {
            if (_unit.Visitantes.Delete(visitante)) return RedirectToAction("Index");
            return PartialView(_unit.Visitantes.GetById(visitante.Id));
        }
        public PartialViewResult Details(int id)
        {
            return PartialView("_Details", _unit.Visitantes.GetById(id));
        }
        [Route("CountPages/{rowSize:int}")]
        public int CountPage(int rowSize)
        {
            var totalRecords = _unit.Visitantes.Count(); //El total de registros
            return totalRecords % rowSize != 0 ? (totalRecords / rowSize) + 1 : totalRecords / rowSize;
        }
        [Route("List/{page:int}/{rows:int}")]
        public PartialViewResult List(int page, int rows)
        {
            if (page <= 0 || rows <= 0) return PartialView(new List<Oficinas>());
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return PartialView("_List", _unit.Visitantes.PagedList(startRecord, endRecord));
        }
    }
}