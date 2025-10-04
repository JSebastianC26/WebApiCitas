using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiCitas.Interfaces;

namespace WebApiCitas.Controllers
{
    [RoutePrefix("api/doctors")]
    public class DoctorsController : ApiController
    {
        private readonly IDoctorService _doctorService;

        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        /// <summary>
        /// GET /api/doctors/{id}
        /// Obtiene información de un médico
        /// </summary>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetDoctor(int id)
        {
            var doctor = await _doctorService.GetDoctorById(id);

            if (doctor == null)
                return NotFound();

            return Ok(doctor);
        }

        /// <summary>
        /// GET /api/doctors
        /// Lista todos los médicos disponibles
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetDoctors([FromUri] string specialty = null)
        {
            var doctors = await _doctorService.GetDoctors(specialty);
            return Ok(doctors);
        }
    }
}
