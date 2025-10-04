using System;

namespace WebApiCitas.Models
{
    /// <summary>
    /// Representa la solicitud de login con usuario y clave.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Usuario para el login
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Clave de acceso para el login
        /// </summary>
        public string Password { get; set; }
    }
}