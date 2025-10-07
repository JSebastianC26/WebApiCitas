using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiCitas.Models;

namespace WebApiCitas.Interfaces
{
    /// <summary>
    /// Interface de conexion con el repositorio para citas.
    /// </summary>
    public interface IAppointmentRepository
    {
        /// <summary>
        /// Tarea de crear citas.
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        Task<Appointment> CreateAppointment(Appointment appointment);

        /// <summary>
        /// Obtener cita por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Appointment> GetAppointmentById(int id);

        /// <summary>
        /// Actualizar cita.
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        Task UpdateAppointment(Appointment appointment);

        /// <summary>
        /// Obtener citas de un paciente.
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns></returns>
        Task<List<Appointment>> GetAppointmentsByPatient(int patientId);

        /// <summary>
        /// Obtener citas 
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<List<Appointment>> GetAppointmentsByDoctor(int doctorId, DateTime date);
    }
}
