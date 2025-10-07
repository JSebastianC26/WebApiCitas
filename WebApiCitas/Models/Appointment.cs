using System;

namespace WebApiCitas.Models
{
    /// <summary>
    /// Cita
    /// </summary>
    public class Appointment
    {
        /// <summary>
        /// Identificador único de la cita.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificacion del paciente asociado a la cita.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// Medico asociado a la cita.
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// Fecha y hora de la cita.
        /// </summary>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// Motivo de la cita.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Estado de la cita. (e.g., Scheduled, Completed, Canceled)
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Fecha de creacion de la cita
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Fecha de actualizacion ed la cita.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}