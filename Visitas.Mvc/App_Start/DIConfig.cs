using log4net;
using log4net.Core;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Visitas.Repositories.Dapper.Implementaciones;
using Visitas.UnitOfWork;

namespace Visitas.Mvc.App_Start
{
    public class DIConfig
    {
        //Es estatico para que sea accesible directamente
        public static void ConfigureInjector()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Register<IUnitOfWork>(() =>
            new VisitasUnitOfWork(ConfigurationManager.ConnectionStrings["VisitasConnection"].ToString()));
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            /*Inyeccion del log4net*/
            container.RegisterConditional(typeof(ILog), c => typeof(Log4NetAdapter<>).MakeGenericType(
                c.Consumer.ImplementationType), Lifestyle.Singleton, c => true);
            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
    }
    //Inyeccion del Log4net
    public sealed class Log4NetAdapter<T> : LogImpl
    {
        public Log4NetAdapter() : base(LogManager.GetLogger(typeof(T)).Logger) { }
    }
}