using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using Microsoft.Owin;
using Owin;
using Visitas.WebApi.App_Start;
using Visitas.WebApi.Handlers;

[assembly: OwinStartup(typeof(Visitas.WebApi.Startup))]

namespace Visitas.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //Configuracion del Log
            log4net.Config.XmlConfigurator.Configure();
            var log = log4net.LogManager.GetLogger(typeof(Startup));
            log.Debug("Log esta habilitado");

            var config = new HttpConfiguration();

            config.Services.Replace(typeof(IExceptionHandler), new GlobalExceptionHandler());//Para el handler (que es para el log)

            DIConfig.ConfigureInjector(config);
            //app.UseCors(CorsOptions.AllowAll); //Esto es por haber instalado el Microsoft.Owin.Cors (para que acepte origines cruzados)
            TokenConfig.ConfigureOAuth(app, config); //Esto es para el Token
            RouteConfig.Register(config);
            WebApiConfig.Configure(config); //Para la compresion

            app.UseWebApi(config);
        }
    }
}
