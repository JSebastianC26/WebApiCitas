using System.Threading.Tasks;
using WebApiCitas.Models;

namespace WebApiCitas.Interfaces
{
    /// <summary>
    /// Interface de servicio para doctores.
    /// </summary>
    public interface IDoctorService
    {
        /// <summary>
        /// Obtner información de un médico por su Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Doctor> GetDoctorById(int id);

        /// <summary>
        /// Obtener lista de médicos, opcionalmente filtrados por especialidad.
        /// </summary>
        /// <param name="specialty"></param>
        /// <returns></returns>
        Task<System.Collections.Generic.List<Doctor>> GetDoctors(string specialty);
    }
}