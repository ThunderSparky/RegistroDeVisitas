using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visitas.Mvc.ActionFilters;
using Visitas.Mvc.ExternalServices;
using Visitas.UnitOfWork;

namespace Visitas.Mvc.Controllers
{
    [Authorize]
    [ErrorActionFilter]
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork _unit;
        protected readonly ILog _log;//Para el log4net
        protected readonly IExternalAPIToken _externalAPIToken; //para la inyeccion del token

        public BaseController(ILog log, IUnitOfWork unit, IExternalAPIToken externalAPIToken)
        {
            _log = log;
            _unit = unit;
            _externalAPIToken = externalAPIToken;
        }
    }
}