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
    [RoutePrefix("CargoLaboral")]
    public class CargoLaboralController : BaseController
    {
        public CargoLaboralController(ILog log, IUnitOfWork unit, IExternalAPIToken externalAPIToken) : base(log, unit, externalAPIToken)
        {

        }
        public ActionResult Index()
        {
            return View(_unit.CargosLaborales.GetList());
        }
        public PartialViewResult Create()
        {
            return PartialView("_Create", new CargosLaborales());
        }
        [HttpPost]
        public ActionResult Create(CargosLaborales cargolaboral)
        {
            if (ModelState.IsValid)
            {
                _unit.CargosLaborales.Insert(cargolaboral);
                return RedirectToAction("Index");
            }
            return PartialView("_Create", cargolaboral);
        }
        public PartialViewResult Edit(int id)
        {
            return PartialView("_Edit", _unit.CargosLaborales.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(CargosLaborales cargolaboral)
        {
            if (_unit.CargosLaborales.Update(cargolaboral)) return RedirectToAction("Index");

            return PartialView("_Edit", cargolaboral);
        }
        public PartialViewResult Delete(int id)
        {
            return PartialView("_Delete", _unit.CargosLaborales.GetById(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(CargosLaborales cargolaboral)
        {
            if (_unit.CargosLaborales.Delete(cargolaboral)) return RedirectToAction("Index");
            return PartialView(_unit.CargosLaborales.GetById(cargolaboral.Id));
        }
        public PartialViewResult Details(int id)
        {
            return PartialView("_Details", _unit.CargosLaborales.GetById(id));
        }
        [Route("CountPages/{rowSize:int}")]
        public int CountPage(int rowSize)
        {
            var totalRecords = _unit.CargosLaborales.Count(); //El total de registros
            return totalRecords % rowSize != 0 ? (totalRecords / rowSize) + 1 : totalRecords / rowSize;
        }
        [Route("List/{page:int}/{rows:int}")]
        public PartialViewResult List(int page, int rows)
        {
            if (page <= 0 || rows <= 0) return PartialView(new List<Oficinas>());
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return PartialView("_List", _unit.CargosLaborales.PagedList(startRecord, endRecord));
        }
    }
}