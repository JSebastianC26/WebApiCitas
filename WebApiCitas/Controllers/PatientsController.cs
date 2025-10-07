using System.Threading.Tasks;
using System.Web.Http;
using WebApiCitas.Interfaces;
using WebApiCitas.Models;

namespace WebApiCitas.Controllers
{
    /// <summary>
    /// Pacientes
    /// </summary>
    [Authorize]
    [RoutePrefix("api/patients")]
    public class PatientsController : ApiController
    {
        private readonly IPatientService _patientService;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="patientService"></param>
        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        /// <summary>
        /// GET /api/patients/{id}
        /// Obtiene información de un paciente
        /// </summary>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetPatient(int id)
        {
            var patient = await _patientService.GetPatientById(id);

            if (patient == null)
                return NotFound();

            return Ok(patient);
        }

        /// <summary>
        /// POST /api/patients
        /// Registra un nuevo paciente
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreatePatient([FromBody] CreatePatientRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patient = await _patientService.CreatePatient(request);
            return Created($"api/patients/{patient.Id}", patient);
        }
    }
}
