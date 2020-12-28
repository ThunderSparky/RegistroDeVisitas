using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Visitas.Models;
using Visitas.Mvc.ExternalServices;
using Visitas.UnitOfWork;

namespace Visitas.Mvc.Controllers
{
    [RoutePrefix("Oficina")]
    public class OficinaController : BaseController
    {
        public OficinaController(ILog log, IUnitOfWork unit, IExternalAPIToken externalAPIToken) : base(log, unit, externalAPIToken)
        {
            
        }
        public ActionResult Index()
        {
            return View(_unit.Oficinas.GetList());
        }
        public PartialViewResult Create()
        {
            ViewBag.Estado = new List<Estado>() { new Estado() { Id = "1", Nombre = "Activo" }, new Estado() { Id = "0", Nombre = "Inactivo" } };
            ViewBag.Trabajador = _unit.Trabajadores.GetList().OrderBy(s => s.No_Trabajador).Select(p => new SelectListItem { 
                Text = p.No_Trabajador + " "+ p.Ap_Paterno_Trabajador + " " + p.Ap_Materno_Trabajador,
                Value = p.No_Trabajador + " " + p.Ap_Paterno_Trabajador + " " + p.Ap_Materno_Trabajador
            }); //Para traer la lista de los trabajadores
            return PartialView("_Create",new Oficinas());
        }
        [HttpPost]
        public ActionResult Create(Oficinas oficina)
        {
            //if (ModelState.IsValid)
            //{
            //    _unit.Oficinas.Insert(oficina);
            //    return RedirectToAction("Index");
            //}
            //return PartialView("_Create",oficina);

            /*Utilizando la inyeccion de dependencia para obtener el token y consumir el webapi*/
            if (ModelState.IsValid)
            {
                var httpClient = new HttpClient();

                //Paso 1: consultar el token con inyección de dependencia
                var tokenString = _externalAPIToken.GetExternalAPIToken();

                //Paso 2: Consumir el servicio
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

                var content = JsonConvert.SerializeObject(oficina);
                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var json = httpClient.PostAsync("https://localhost:44329/oficina", byteContent);

                return RedirectToAction("Index");
            }
            return PartialView("_Create", oficina);
        }
        public PartialViewResult Edit(int id)
        {
            ViewBag.Trabajador = _unit.Trabajadores.GetList().OrderBy(s => s.No_Trabajador).Select(p => new SelectListItem
            {
                Text = p.No_Trabajador + " " + p.Ap_Paterno_Trabajador + " " + p.Ap_Materno_Trabajador,
                Value = p.No_Trabajador + " " + p.Ap_Paterno_Trabajador + " " + p.Ap_Materno_Trabajador
            }); //Para traer la lista de los trabajadores
            return PartialView("_Edit", _unit.Oficinas.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(Oficinas oficina)
        {
            //if (_unit.Oficinas.Update(oficina)) return RedirectToAction("Index");

            //return PartialView("_Edit", oficina);

            /*Consumiendo el webapi*/
            var httpClient = new HttpClient();
            //1.Recuperamos el token
            var context = Request.GetOwinContext();
            var tokenString = context.Authentication.User.Claims.ElementAt(4).Value;
            //2.Consumir el servicio
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);//colocamos la cabecera
            //para mandar la información en el body
            var content = JsonConvert.SerializeObject(oficina);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var json = httpClient.PutAsync("https://localhost:44329/oficina", byteContent);
            var result = json.Result.Content.ReadAsStringAsync().Result;
            var contentResult = JsonConvert.DeserializeObject<Dictionary<string, Boolean>>(result);

            if (contentResult["status"]) return RedirectToAction("Index");
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
        public async Task<PartialViewResult> List(int page, int rows)
        {
            //if (page <= 0 || rows <= 0) return PartialView(new List<Oficinas>());
            //var startRecord = ((page - 1) * rows) + 1;
            //var endRecord = page * rows;
            //return PartialView("_List", _unit.Oficinas.PagedList(startRecord, endRecord));

            /*Si se consume el web api*/
            //Paso 1: Solicitar el token
            var httpClient = new HttpClient();
            var credential = new Dictionary<string, string>
            {
                {"grant_type", "password" },
                {"username", "usi25@apci.gob.pe"},
                {"password", "Julinho123" }
            };
            var response = await httpClient.PostAsync("https://localhost:44329/token",
                new FormUrlEncodedContent(credential));
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var tokenDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);

            //Paso 2: Consumir el servicio
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenDictionary["access_token"]);//Agregamos a la cabecera el token

            var json = await httpClient.GetStringAsync("https://localhost:44329/oficina/pagina/" + page + "/" + rows);

            List<Oficinas> lstOficinas = JsonConvert.DeserializeObject<List<Oficinas>>(json);

            return PartialView("_List", lstOficinas);
        }
    }
}