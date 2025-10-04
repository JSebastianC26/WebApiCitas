using System.Threading.Tasks;
using WebApiCitas.Models;

namespace WebApiCitas.Interfaces
{
    public interface IAppointmentOrchestrator
    {
        Task<OrchestrationResult> CoordinateAppointmentCreation(AppointmentRequest request);
        Task<OrchestrationResult> CoordinateAppointmentCancellation(int appointmentId);
    }
}