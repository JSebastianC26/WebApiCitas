using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Filters;
using WebApiCitas.Services;

namespace WebApiCitas
{
    /// <summary>
    ///  WebApiConfig configura las rutas, formatos y filtros globales para la Web API.
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registra la configuración de Web API.
        /// </summary>
        /// <param name="config"></param>
        public static void Register(HttpConfiguration config)
        {
            // Habilitar CORS (si es necesario)
            config.EnableCors();

            // ===== CONFIGURACIÓN DE RUTAS =====

            // Habilitar attribute routing (para usar [Route])
            config.MapHttpAttributeRoutes();

            // Ruta por defecto (fallback)
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // ===== CONFIGURACIÓN DE FORMATO JSON =====

            // Configurar JSON como formato predeterminado
            config.Formatters.JsonFormatter.SupportedMediaTypes
                .Add(new MediaTypeHeaderValue("text/html"));

            // Configurar serialización JSON
            var jsonSettings = config.Formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSettings.NullValueHandling = NullValueHandling.Ignore;

            // Configurar formato de fechas
            jsonSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

            // ===== FILTROS GLOBALES =====

            // Agregar manejo de excepciones global
            config.Filters.Add(new GlobalExceptionFilter());

            // Agregar validación de modelo automática
            config.Filters.Add(new ValidateModelStateFilter());
        }
    }

    /// <summary>
    /// GlobalExceptionFilter maneja excepciones no controladas y devuelve respuestas JSON estandarizadas.
    /// </summary>
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var exception = context.Exception;

            // Log the exception (implementar logger real)
            System.Diagnostics.Debug.WriteLine($"Error: {exception.Message}");

            var response = new
            {
                success = false,
                message = "Ha ocurrido un error interno",
                error = exception.Message,
                stackTrace = exception.StackTrace
            };

            context.Response = context.Request.CreateResponse(
                System.Net.HttpStatusCode.InternalServerError,
                response);

            base.OnException(context);
        }
    }

    // ===== FILTRO DE VALIDACIÓN DE MODELO =====
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var errors = actionContext.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new
                    {
                        field = x.Key,
                        errors = x.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    })
                    .ToList();

                var response = new
                {
                    success = false,
                    message = "Error de validación",
                    errors = errors
                };

                actionContext.Response = actionContext.Request.CreateResponse(
                    System.Net.HttpStatusCode.BadRequest,
                    response);
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
