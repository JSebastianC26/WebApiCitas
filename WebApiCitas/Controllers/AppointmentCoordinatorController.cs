using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiCitas.Interfaces;
using WebApiCitas.Models;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace WebApiCitas.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api")]
    public class AppointmentCoordinatorController : ApiController
    {
        private readonly IAppointmentOrchestrator _orchestrator;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Constructor del controlador de orquestación de citas médicas.
        /// </summary>
        /// <param name="orchestrator"></param>
        public AppointmentCoordinatorController( IAppointmentOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        /// <summary>
        /// Orquesta la creación de una cita médica
        /// </summary>
        [HttpPost]
        [Route("appointmentCoordinator")]
        public async Task<IHttpActionResult> CreateAppointment([FromBody] AppointmentRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                log.Info($"Iniciando orquestación de cita para paciente: {request.PatientId}");

                // Orquestar el proceso completo
                var result = await _orchestrator.CoordinateAppointmentCreation(request);

                if (result.Success)
                {
                    return Ok(new
                    {
                        success = true,
                        appointmentId = result.AppointmentId,
                        message = "Cita agendada exitosamente",
                        data = result
                    });
                }

                return BadRequest(result.ErrorMessage);
            }
            catch (Exception ex)
            {
                log.Error($"Error al coordinar cita: {ex.Message}", ex);
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Cancela una cita médica existente
        /// </summary>
        [HttpPut]
        [Route("cancel/{appointmentId}")]
        [ResponseType(typeof(RetornoEstandar ))]
        public async Task<IHttpActionResult> CancelAppointment(int appointmentId)
        {
            RetornoEstandar retornoEstandar = new RetornoEstandar();
            try
            {
                log.Info($"Iniciando cancelación de cita: {appointmentId}");

                var result = await _orchestrator.CoordinateAppointmentCancellation(appointmentId);

                if (result.Success)
                {
                    retornoEstandar.MensajeRetorno = "Cita cancelada exitosamente";

                    return Ok(retornoEstandar);
                }
                else
                {
                    retornoEstandar.idRetorno = "1";
                    retornoEstandar.MensajeRetorno = result.ErrorMessage;

                    return Content(HttpStatusCode.BadRequest, retornoEstandar);
                }
            }
            catch (Exception ex)
            {
                retornoEstandar.idRetorno = "1";
                retornoEstandar.MensajeRetorno = $"Error al cancelar cita: {ex.Message}";
                log.Error($"Error al cancelar cita: {ex.Message}", ex);
                return Content(HttpStatusCode.InternalServerError,  retornoEstandar);
            }
        }

    }
}
