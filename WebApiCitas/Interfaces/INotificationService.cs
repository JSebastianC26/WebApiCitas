using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebApiCitas.Interfaces
{
    public interface INotificationService
    {
        Task SendAppointmentConfirmation(string email, string doctorName, DateTime date);
        Task SendCancellationNotification(string email, string doctorName, DateTime date);
        Task SendCustomNotification(string email, string subject, string message);
    }
}