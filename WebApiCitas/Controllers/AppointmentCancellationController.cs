using System.Threading.Tasks;
using System.Web.Http;
using WebApiCitas.Interfaces;

namespace WebApiCitas.Controllers
{
    /// <summary>
    /// Servicio orquestador para cancelacion de citas.
    /// </summary>
    [Authorize]
    [RoutePrefix("api/appointments")]
    public class AppointmentCancellationController : ApiController
    {
        private readonly IAppointmentOrchestrator _orchestrator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="orchestrator"></param>
        public AppointmentCancellationController(IAppointmentOrchestrator orchestrator)
        {
            _orchestrator = orchestrator;
        }

        /// <summary>
        /// PUT /api/appointments/cancel/{id}
        /// Cancela una cita médica existente
        /// </summary>
        [HttpPut]
        [Route("cancel/{id:int}")]
        public async Task<IHttpActionResult> CancelAppointment(int id)
        {
            var result = await _orchestrator.CoordinateAppointmentCancellation(id);

            if (result.Success)
                return Ok(new { message = "Cita cancelada exitosamente" });

            return BadRequest(result.ErrorMessage);
        }
    }
}
