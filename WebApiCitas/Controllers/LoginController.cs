using System.Configuration;
using System.Net;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiCitas.Models;
using WebApiCitas.Services;

namespace WebApiCitas.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        /// <summary>
        /// Metodo de prueba para verificar que el servicio esta activo.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("echoping")]
        public IHttpActionResult EchoPing()
        {
            return Ok(true);
        }

        /// <summary>
        /// Metodo de prueba para verificar que el usuario esta autenticado.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("echouser")]
        public IHttpActionResult EchoUser()
        {
            var identity = Thread.CurrentPrincipal.Identity;
            return Ok($" IPrincipal-user: {identity.Name} - IsAuthenticated: {identity.IsAuthenticated}");
        }

        /// <summary>
        /// Metodo para autenticar usuarios y generar un token JWT
        /// </summary>
        /// <param name="plogin"></param>
        /// <returns></returns>
        /// <exception cref="HttpResponseException"></exception>
        [HttpPost]
        [Route("authenticate")]
        [ResponseType(typeof(LoginResponse))]
        public IHttpActionResult Authenticate(LoginRequest plogin)
        {
            RetornoEstandar retornoEstandar = new RetornoEstandar();
            LoginResponse login = new LoginResponse();

            if (plogin == null)
            {
                retornoEstandar.idRetorno = "1";
                retornoEstandar.MensajeRetorno = "Error: No se recibieron datos de autenticacion.";
                login.Retorno = retornoEstandar;
                return BadRequest();
            }

            string passService = ConfigurationManager.AppSettings["passService"];
            string userService = ConfigurationManager.AppSettings["userService"];

            bool isCredentialValid = (plogin.Password == passService && plogin.Username == userService); // revisar metodo para validar credenciales ***** 

            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(plogin.Username);
                login.Token = token;
                return Ok(login);
            }
            else
            {
                retornoEstandar.idRetorno = "1";
                retornoEstandar.MensajeRetorno = "Error: Credenciales invalidas.";
                login.Retorno = retornoEstandar;
                return Content(HttpStatusCode.Unauthorized, login);
            }
        }
    }
}
