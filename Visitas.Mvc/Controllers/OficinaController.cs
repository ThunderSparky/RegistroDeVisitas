using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visitas.Models;
using Visitas.UnitOfWork;

namespace Visitas.Mvc.Controllers
{
    [RoutePrefix("Oficina")]
    public class OficinaController : BaseController
    {
        public OficinaController(ILog log, IUnitOfWork unit) : base(log, unit)
        {
            
        }
        public ActionResult Index()
        {
            return View(_unit.Oficinas.GetList());
        }
        public PartialViewResult Create()
        {
            return PartialView("_Create",new Oficinas());
        }
        [HttpPost]
        public ActionResult Create(Oficinas oficina)
        {
            if (ModelState.IsValid)
            {
                _unit.Oficinas.Insert(oficina);
                return RedirectToAction("Index");
            }
            return PartialView("_Create",oficina);
        }
        public PartialViewResult Edit(int id)
        {
            return PartialView("_Edit", _unit.Oficinas.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(Oficinas oficina)
        {
            if (_unit.Oficinas.Update(oficina)) return RedirectToAction("Index");

            return PartialView("_Edit", oficina);
        }
        public PartialViewResult Delete(int id)
        {
            return PartialView("_Delete", _unit.Oficinas.GetById(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(Oficinas oficina)
        {
            if (_unit.Oficinas.Delete(oficina)) return RedirectToAction("Index");
            return PartialView(_unit.Oficinas.GetById(oficina.Id));
        }
        public PartialViewResult Details(int id)
        {
            return PartialView("_Details", _unit.Oficinas.GetById(id));
        }
        [Route("CountPages/{rowSize:int}")]
        public int CountPage(int rowSize)
        {
            var totalRecords = _unit.Oficinas.Count(); //El total de registros
            return totalRecords % rowSize != 0 ? (totalRecords / rowSize) + 1 : totalRecords / rowSize;
        }
        [Route("List/{page:int}/{rows:int}")]
        public PartialViewResult List(int page, int rows)
        {
            if (page <= 0 || rows <= 0) return PartialView(new List<Oficinas>());
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return PartialView("_List", _unit.Oficinas.PagedList(startRecord, endRecord));
        }
    }
}