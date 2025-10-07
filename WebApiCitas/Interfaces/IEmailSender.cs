using System.Threading.Tasks;

namespace WebApiCitas.Interfaces
{
    /// <summary>
    /// UInterface de envio de emails.
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Envio de correos    electrónicos.
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        Task SendEmailAsync(string to, string subject, string body);
    }
}
