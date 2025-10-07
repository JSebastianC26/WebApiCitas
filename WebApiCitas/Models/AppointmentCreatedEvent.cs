using System;

namespace WebApiCitas.Models
{
    /// <summary>
    /// Creacion de cita
    /// </summary>
    public class AppointmentCreatedEvent
    {
        /// <summary>
        /// Id de la cita.
        /// </summary>
        public int AppointmentId { get; set; }

        /// <summary>
        /// Identidicador del paciente asociado a la cita.
        /// </summary>
        public int PatientId { get; set; }

        /// <summary>
        /// id del medico asociado a la cita.
        /// </summary>
        public int DoctorId { get; set; }

        /// <summary>
        /// Fecha de la cita.
        /// </summary>
        public DateTime AppointmentDate { get; set; }

        /// <summary>
        /// Fecha y hora del evento.
        /// </summary>
        public DateTime Timestamp { get; set; }
    }
}