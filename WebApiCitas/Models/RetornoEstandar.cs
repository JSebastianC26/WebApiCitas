namespace WebApiCitas.Models
{
    /// <summary>
    /// Retorno estandar para las respuestas de la API.
    /// </summary>
    public class RetornoEstandar
    {
        /// <summary>
        /// Constructor que inicializa los valores por defecto.
        /// </summary>
        public RetornoEstandar()
        {
            idRetorno = "0";
            MensajeRetorno = "Operacion Exitosa";
        }
        /// <summary>
        /// Identificador del codigo de respuesta.
        /// Error = 1, exito = 0, alerta = 2
        /// </summary>
        public string idRetorno { get; set; }

        /// <summary>
        /// Descripcion del codigo de respuesta.
        /// </summary>
        public string MensajeRetorno { get; set; }
        
    }
}