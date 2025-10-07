using System;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiCitas.Interfaces;
using WebApiCitas.Models;

namespace WebApiCitas.Controllers
{
    /// <summary>
    /// Citas medicas
    /// </summary>
    [Authorize]
    [RoutePrefix("api/appointments")]
    public class AppointmentsController : ApiController
    {
        private readonly IScheduleService _scheduleService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="scheduleService"></param>
        public AppointmentsController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        /// <summary>
        /// POST /api/appointments/schedule
        /// Agenda una cita médica (llama internamente al orquestador)
        /// </summary>
        [HttpPost]
        [Route("schedule")]
        public async Task<IHttpActionResult> ScheduleAppointment([FromBody] ScheduleRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var isAvailable = await _scheduleService.CheckAvailability(
                request.DoctorId,
                request.AppointmentDate);

            if (!isAvailable)
                return BadRequest("Horario no disponible");

            // Aquí se puede reservar el slot temporalmente
            var scheduled = await _scheduleService.ReserveTimeSlot(
                request.DoctorId,
                request.AppointmentDate);

            return Ok(new { success = scheduled });
        }

        /// <summary>
        /// GET /api/appointments/availability
        /// Consulta disponibilidad de horarios
        /// </summary>
        [HttpGet]
        [Route("availability")]
        public async Task<IHttpActionResult> GetAvailability(
            [FromUri] int doctorId,
            [FromUri] DateTime date)
        {
            var availability = await _scheduleService.GetAvailableSlots(doctorId, date);
            return Ok(availability);
        }
    }
}
