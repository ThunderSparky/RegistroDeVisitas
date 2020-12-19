using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visitas.Models;
using Visitas.Mvc.ExternalServices;
using Visitas.UnitOfWork;

namespace Visitas.Mvc.Controllers
{
    [RoutePrefix("Visita")]
    public class VisitaController : BaseController
    {
        public VisitaController(ILog log, IUnitOfWork unit, IExternalAPIToken externalAPIToken) : base(log, unit, externalAPIToken)
        {

        }
        public ActionResult Index()
        {
            return View(_unit.Visitass.GetList());
        }
        public PartialViewResult Create()
        {
            ViewBag.Estado = new List<Estado>() { new Estado() { Id = "1", Nombre = "Activo" }, new Estado() { Id = "0", Nombre = "Inactivo" } };
            ViewBag.Visitante = _unit.Visitantes.GetList(); //Para traer la lista de los visitantes
            ViewBag.Trabajador = _unit.Trabajadores.GetList(); //Para traer la lista de los trabajadores
            return PartialView("_Create", new Visitass());
        }
        [HttpPost]
        public ActionResult Create(Visitass visita)
        {
            if (ModelState.IsValid)
            {
                _unit.Visitass.Insert(visita);
                return RedirectToAction("Index");
            }
            return PartialView("_Create", visita);
        }
        public PartialViewResult Edit(int id)
        {
            ViewBag.Visitante = _unit.Visitantes.GetList(); //Para traer la lista de los visitantes
            ViewBag.Trabajador = _unit.Trabajadores.GetList(); //Para traer la lista de los trabajadores
            return PartialView("_Edit", _unit.Visitass.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(Visitass visita)
        {
            if (_unit.Visitass.Update(visita)) return RedirectToAction("Index");

            return PartialView("_Edit", visita);
        }
        public PartialViewResult Delete(int id)
        {
            return PartialView("_Delete", _unit.Visitass.GetById(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(Visitass visita)
        {
            if (_unit.Visitass.Delete(visita)) return RedirectToAction("Index");
            return PartialView(_unit.Visitass.GetById(visita.Id));
        }
        public PartialViewResult Details(int id)
        {
            
            return PartialView("_Details", _unit.Visitass.GetById(id));
        }
        [Route("CountPages/{rowSize:int}")]
        public int CountPage(int rowSize)
        {
            var totalRecords = _unit.Visitass.Count(); //El total de registros
            return totalRecords % rowSize != 0 ? (totalRecords / rowSize) + 1 : totalRecords / rowSize;
        }
        [Route("List/{page:int}/{rows:int}")]
        public PartialViewResult List(int page, int rows)
        {
            if (page <= 0 || rows <= 0) return PartialView(new List<Oficinas>());
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return PartialView("_List", _unit.Visitass.PagedList(startRecord, endRecord));
        }
        [Route("ListByFilters/{visitainicio}/{visitafin}")]
        public PartialViewResult ListByFilters(string visitainicio, string visitafin)
        {
            List<Visitass> lstVisitas = new List<Visitass>();

            if (!visitainicio.Equals("dd/mm/aaaa") && !visitafin.Equals("dd/mm/aaaa"))
            {
                lstVisitas = _unit.Visitass.GetByFecha(visitainicio, visitafin);
            }
            //else if (!institutoNum.Equals("-") && institutoName.Equals("-"))
            //{
            //    //var instituto = _unit.Institutos.GetByNum(institutoNum);
            //    //lstInstitutos.Add(instituto);
            //    lstInstitutos = _unit.Institutos.GetByNum(institutoNum);
            //}
            //else if (!institutoNum.Equals("-") && !institutoName.Equals("-"))
            //{
            //    lstInstitutos = _unit.Institutos.GetByNameAndNum(institutoName, institutoNum);
            //}
            return PartialView("_List", lstVisitas);
        }
    }
}