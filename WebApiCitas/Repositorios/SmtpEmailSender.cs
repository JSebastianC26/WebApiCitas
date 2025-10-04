using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiCitas.Interfaces;

namespace WebApiCitas.Repositorios
{
    public class SmtpEmailSender : IEmailSender
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPassword;

        public SmtpEmailSender()
        {
            _smtpHost = ConfigurationManager.AppSettings["SMTP:Host"] ?? "smtp.gmail.com";
            _smtpPort = int.Parse(ConfigurationManager.AppSettings["SMTP:Port"] ?? "587");
            _smtpUser = ConfigurationManager.AppSettings["SMTP:User"];
            _smtpPassword = ConfigurationManager.AppSettings["SMTP:Password"];
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                using (var client = new System.Net.Mail.SmtpClient(_smtpHost, _smtpPort))
                {
                    client.EnableSsl = true;
                    client.Credentials = new System.Net.NetworkCredential(_smtpUser, _smtpPassword);

                    var message = new System.Net.Mail.MailMessage
                    {
                        From = new System.Net.Mail.MailAddress(_smtpUser),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };

                    message.To.Add(to);

                    await client.SendMailAsync(message);
                    Console.WriteLine($"[Email] Enviado a: {to}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Email] Error: {ex.Message}");
                throw;
            }
        }
    }

}