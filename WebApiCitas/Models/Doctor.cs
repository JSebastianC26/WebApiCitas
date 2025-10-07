namespace WebApiCitas.Models
{
    /// <summary>
    /// Modelo de datos para un doctor.
    /// </summary>
    public class Doctor
    {
        /// <summary>
        /// Id del doctor.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del doctor.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Especialidad del doctor.
        /// </summary>
        public string Specialty { get; set; }
    }
}