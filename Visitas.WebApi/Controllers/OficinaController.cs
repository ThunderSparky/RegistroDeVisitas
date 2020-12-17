using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Visitas.Models;
using Visitas.UnitOfWork;

namespace Visitas.WebApi.Controllers
{
    [RoutePrefix("oficina")]
    public class OficinaController : BaseController
    {
        public OficinaController(IUnitOfWork unit, ILog log): base(unit, log)
        {
            _log.Info($"{typeof(OficinaController)} in ejecucion");
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("error")]
        public IHttpActionResult CreateError()
        {
            throw new System.Exception("Es un error de unhandel error");
        }
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (id == 0 || id <=0) return BadRequest();
            //Si tiene informacion, pues la retornamos
            return Ok(_unit.Oficinas.GetById(id));
        }
        [Route("")]
        [HttpPost]
        public IHttpActionResult GuardarOficina(Oficinas oficina)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var id = _unit.Oficinas.Insert(oficina);
            return Ok(new { id = id });
        }
        [Route("")]
        [HttpPut]
        public IHttpActionResult ActualizarOficina(Oficinas oficina)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!_unit.Oficinas.Update(oficina)) return BadRequest("incorrecto id");
            return Ok(new { status = true });
        }
        [Route("")]
        [HttpDelete]
        public IHttpActionResult Delete(Oficinas oficina)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = _unit.Oficinas.Delete(oficina);
            if (result) return Ok(new { delete = true });
            else return BadRequest("No se eliminó la oficina");
        }
        [Route("lista")]
        [HttpGet]
        public IHttpActionResult GetList()
        {
            return Ok(_unit.Oficinas.GetList());
        }
        [Route("pagina/{page:int}/{rows:int}")]
        [HttpGet]
        public IHttpActionResult GetList(int page, int rows)
        {
            if (page <= 0 || rows <= 0) return Ok(new List<Oficinas>());
            var startRecord = ((page - 1) * rows) + 1;
            var endRecord = page * rows;
            return Ok(_unit.Oficinas.PagedList(startRecord, endRecord));
        }
    }
}