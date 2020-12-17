using log4net;
using log4net.Core;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using SimpleInjector.Lifestyles;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using Visitas.Repositories.Dapper.Implementaciones;
using Visitas.UnitOfWork;

namespace Visitas.WebApi.App_Start
{
    public class DIConfig
    {
        //Es estatico para que sea accesible directamente
        public static void ConfigureInjector(HttpConfiguration config)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            container.Register<IUnitOfWork>(() =>
                new VisitasUnitOfWork(ConfigurationManager.ConnectionStrings["VisitasConnection"].ToString()));

            /*Inyeccion del log4net*/
            container.RegisterConditional(typeof(ILog), c => typeof(Log4NetAdapter<>).MakeGenericType(
                c.Consumer.ImplementationType), Lifestyle.Singleton, c => true);

            container.RegisterWebApiControllers(config);
            container.Verify();
            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
    //Inyeccion del Log4net
    public sealed class Log4NetAdapter<T> : LogImpl
    {
        public Log4NetAdapter() : base(LogManager.GetLogger(typeof(T)).Logger) { }
    }
}