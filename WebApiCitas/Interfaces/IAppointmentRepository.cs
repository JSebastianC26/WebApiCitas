using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCitas.Models;

namespace WebApiCitas.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment> CreateAppointment(Appointment appointment);
        Task<Appointment> GetAppointmentById(int id);
        Task UpdateAppointment(Appointment appointment);
        Task<List<Appointment>> GetAppointmentsByPatient(int patientId);
        Task<List<Appointment>> GetAppointmentsByDoctor(int doctorId, DateTime date);
    }
}
