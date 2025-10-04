using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebApiCitas.Services;

namespace WebApiCitas
{

    /// <summary>
    /// inicializa y configura la aplicación Web API al arrancar.
    /// </summary>
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Configures and initializes the application at startup.
        /// </summary>
        /// <remarks>This method is called automatically when the application starts. It registers areas, 
        /// configures Web API routes, applies global filters, sets up route mappings, and bundles  resources such as
        /// scripts and styles. Ensure that all configuration methods invoked  within this method are properly defined
        /// to avoid runtime errors.</remarks>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configure(DependencyConfig.Configure);
        }
        protected void Application_Error(object sender, System.EventArgs e)
        {
            var exception = Server.GetLastError();

            // Log del error
            System.Diagnostics.Debug.WriteLine($"Error no manejado: {exception?.Message}");

            // Opcional: Registrar en base de datos o servicio de logging
        }

    }
}
