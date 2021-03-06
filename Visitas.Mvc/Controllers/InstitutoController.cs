﻿using log4net;
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
    [RoutePrefix("Instituto")]
    public class InstitutoController : BaseController
    {
        public InstitutoController(ILog log, IUnitOfWork unit, IExternalAPIToken externalAPIToken) : base(log, unit, externalAPIToken)
        {

        }
        public ActionResult Index()
        {
            return View(_unit.Institutos.GetList());
        }
        public PartialViewResult Create()
        {
            ViewBag.Estado = new List<Estado>() { new Estado() { Id = "1", Nombre = "Activo" }, new Estado() { Id = "0", Nombre = "Inactivo" } };
            return PartialView("_Create", new Institutos());
        }
        [HttpPost]
        public ActionResult Create(Institutos instituto)
        {
            if (ModelState.IsValid)
            {
                _unit.Institutos.Insert(instituto);
                return RedirectToAction("Index");
            }
            return PartialView("_Create", instituto);
        }
        public PartialViewResult Edit(int id)
        {
            return PartialView("_Edit", _unit.Institutos.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(Institutos instituto)
        {
            if (_unit.Institutos.Update(instituto)) return RedirectToAction("Index");

            return PartialView("_Edit", instituto);
        }
        public PartialViewResult Delete(int id)
        {
            return PartialView("_Delete", _unit.Institutos.GetById(id));
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeletePost(Institutos instituto)
        {
            if (_unit.Institutos.Delete(instituto)) return RedirectToAction("Index");
            return PartialView(_unit.Institutos.GetById(instituto.Id));
        }
        public PartialViewResult Details(int id)
        {
            return PartialView("_Details", _unit.Institutos.GetById(id));
        }
        [Route("CountPages/{rowSize:int}")]
        public int CountPage(int rowSize)
        {
            var totalRecords = _unit.Institutos.Count(); //El total de registros
            return totalRecords % rowSize != 0 ? (totalRecords / rowSize) + 1 : totalRecords / rowSize;
        }
        [Route("List/{page:int}/{rows:int}")]
        public PartialViewResult List(int page, int rows)
        {
            if (page <= 0 || rows <= 0) return PartialView(new List<Oficinas>());
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return PartialView("_List", _unit.Institutos.PagedList(startRecord, endRecord));
        }
        [Route("ListByFilters/{institutoName}/{institutoNum}")]
        public PartialViewResult ListByFilters(string institutoName, string institutoNum)
        {
            List<Institutos> lstInstitutos = new List<Institutos>();

            if (!institutoName.Equals("-") && institutoNum.Equals("-"))
            {
                lstInstitutos = _unit.Institutos.GetByName(institutoName);
                
            }else if(!institutoNum.Equals("-") && institutoName.Equals("-"))
            {
                //var instituto = _unit.Institutos.GetByNum(institutoNum);
                //lstInstitutos.Add(instituto);
                lstInstitutos = _unit.Institutos.GetByNum(institutoNum);
            }else if(!institutoNum.Equals("-") && !institutoName.Equals("-"))
            {
                lstInstitutos = _unit.Institutos.GetByNameAndNum(institutoName, institutoNum);
            }
            return PartialView("_List", lstInstitutos);
        }
    }
}