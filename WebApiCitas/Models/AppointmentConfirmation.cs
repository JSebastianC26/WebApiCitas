using System;

namespace WebApiCitas.Models
{
    /// <summary>
    /// Clase que representa la confirmación de una cita médica.
    /// </summary>
    public class AppointmentConfirmation
    {
        /// <summary>
        /// Código único de la cita médica.
        /// </summary>
        public int AppointmentId { get; set; }

        /// <summary>
        /// Fecha y hora programada de la cita médica.
        /// </summary>
        public DateTime ScheduledDate { get; set; }

        /// <summary>
        /// Nombre del médico asignado a la cita.
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// Nombre del paciente que ha reservado la cita.
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// Notificación enviada al paciente.
        /// </summary>
        public bool NotificationSent { get; set; }

        /// <summary>
        /// Pagho procesado.
        /// </summary>
        public bool PaymentProcessed { get; set; }
    }
}