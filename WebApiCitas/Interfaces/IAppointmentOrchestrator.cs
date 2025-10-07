using System.Threading.Tasks;
using WebApiCitas.Models;

namespace WebApiCitas.Interfaces
{
    /// <summary>
    /// Interface de orquestacion de citas
    /// </summary>
    public interface IAppointmentOrchestrator
    {
        /// <summary>
        /// Tarea de orquestar la creacion de una cita.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<OrchestrationResult> CoordinateAppointmentCreation(AppointmentRequest request);

        /// <summary>
        /// Tarea orquestar la cancelacion de una cita. 
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns></returns>
        Task<OrchestrationResult> CoordinateAppointmentCancellation(int appointmentId);
    }
}