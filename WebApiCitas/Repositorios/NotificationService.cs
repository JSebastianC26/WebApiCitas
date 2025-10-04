using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiCitas.Interfaces;

namespace WebApiCitas.Repositorios
{
    public class NotificationService : INotificationService
    {
        private readonly IEmailSender _emailSender;

        public NotificationService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendAppointmentConfirmation(
            string email,
            string doctorName,
            DateTime date)
        {
            var subject = "Confirmación de Cita Médica";
            var body = $@"
                <h2>Cita Confirmada</h2>
                <p>Su cita con el Dr./Dra. {doctorName} ha sido confirmada.</p>
                <p><strong>Fecha y hora:</strong> {date:dd/MM/yyyy HH:mm}</p>
                <p>Por favor llegue 10 minutos antes de su cita.</p>";

            await _emailSender.SendEmailAsync(email, subject, body);
        }

        public async Task SendCancellationNotification(
            string email,
            string doctorName,
            DateTime date)
        {
            var subject = "Cancelación de Cita Médica";
            var body = $@"
                <h2>Cita Cancelada</h2>
                <p>Su cita con el Dr./Dra. {doctorName} programada para el {date:dd/MM/yyyy HH:mm} ha sido cancelada.</p>
                <p>Si desea reagendar, por favor contacte con nosotros.</p>";

            await _emailSender.SendEmailAsync(email, subject, body);
        }

        public async Task SendCustomNotification(string email, string subject, string message)
        {
            await _emailSender.SendEmailAsync(email, subject, message);
        }
    }
}