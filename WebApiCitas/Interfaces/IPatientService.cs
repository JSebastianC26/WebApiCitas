using System.Threading.Tasks;
using WebApiCitas.Models;

namespace WebApiCitas.Interfaces
{
    /// <summary>
    /// Interface de servicio para pacientes.
    /// </summary>
    public interface IPatientService
    {
        /// <summary>
        /// Obtener paciente por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Patient> GetPatientById(int id);

        /// <summary>
        /// Crear un nuevo paciente.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Patient> CreatePatient(CreatePatientRequest request);
    }
}