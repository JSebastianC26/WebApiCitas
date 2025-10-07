using System;

namespace WebApiCitas.Models
{
    /// <summary>
    /// Creación de un nuevo paciente
    /// </summary>
    public class CreatePatientRequest
    {
        /// <summary>
        /// Nombre del paciente
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Correo del paciente
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Telefono del paciente.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Fecha de naciemiento del paciente.q
        /// </summary>
        public DateTime DateOfBirth { get; set; }
    }
}