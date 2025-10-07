namespace WebApiCitas.Models
{
    /// <summary>
    /// Modelo que representa la respuesta de un login.
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Token Unico
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the standard return object containing the result of the operation.
        /// </summary>
        public RetornoEstandar Retorno { get; set; }
    }
}