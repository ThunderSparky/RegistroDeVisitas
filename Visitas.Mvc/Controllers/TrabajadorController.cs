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
    [RoutePrefix("Trabajador")]
    public class TrabajadorController : BaseController
    {
        public TrabajadorController(ILog log, IUnitOfWork unit, IExternalAPIToken externalAPIToken) : base(log, unit, externalAPIToken)
        {

        }
        public ActionResult Index()
        {
            return View(_unit.Trabajadores.GetList());
        }
        public PartialViewResult Create()
        {
            ViewBag.Estado = new List<Estado>() { new Estado() { Id = "1", Nombre = "Activo" }, new Estado() { Id = "0", Nombre = "Inactivo" } };
            ViewBag.Oficinas = _unit.Oficinas.GetList().OrderBy(s => s.No_Oficina); //Para traer la lista de las oficinas
            ViewBag.Cargolaboral = _unit.CargosLaborales.GetList().OrderBy(s => s.No_Cargo_Laboral); //Para traer la lista de los cargos laborales
            return PartialView("_Create", new Trabajadores());
        }
        [HttpPost]
        public ActionResult Create(Trabajadores trabajador)
        {
            if (ModelState.IsValid)
            {
                _unit.Trabajadores.Insert(trabajador);
                return RedirectToAction("Index");
            }
            return PartialView("_Create", trabajador);
        }
        public PartialViewResult Edit(int id)
        {
            ViewBag.Oficinas = _unit.Oficinas.GetList().OrderBy(s => s.No_Oficina); //Para traer la lista de las oficinas
            ViewBag.Cargolaboral = _unit.CargosLaborales.GetList().OrderBy(s => s.No_Cargo_Laboral); //Para traer la lista de los cargos laborales
            return PartialView("_Edit", _unit.Trabajadores.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(Trabajadores trabajador)
        {
            if (_unit.Trabajadores.Update(trabajador)) return RedirectToAction("Index");

            return PartialView("_Edit", trabajador);
        }
        public PartialViewResult Delete(int id)
        {
            return PartialView("_Delete", _unit.Trabajadores.GetById(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(Trabajadores trabajador)
        {
            if (_unit.Trabajadores.Delete(trabajador)) return RedirectToAction("Index");
            return PartialView(_unit.Trabajadores.GetById(trabajador.Id));
        }
        public PartialViewResult Details(int id)
        {
            return PartialView("_Details", _unit.Trabajadores.GetById(id));
        }
        [Route("CountPages/{rowSize:int}")]
        public int CountPage(int rowSize)
        {
            var totalRecords = _unit.Trabajadores.Count(); //El total de registros
            return totalRecords % rowSize != 0 ? (totalRecords / rowSize) + 1 : totalRecords / rowSize;
        }
        [Route("List/{page:int}/{rows:int}")]
        public PartialViewResult List(int page, int rows)
        {
            if (page <= 0 || rows <= 0) return PartialView(new List<Oficinas>());
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return PartialView("_List", _unit.Trabajadores.PagedList(startRecord, endRecord));
        }
    }
}