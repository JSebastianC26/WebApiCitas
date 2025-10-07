using System;
using System.Threading.Tasks;
using WebApiCitas.Interfaces;
using WebApiCitas.Models;

namespace WebApiCitas.Services
{
    /// <summary>
    /// Implementación del orquestador de citas médicas
    /// </summary>
    public class AppointmentOrchestrator : IAppointmentOrchestrator
    {
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IScheduleService _scheduleService;
        private readonly INotificationService _notificationService;
        private readonly IPaymentService _paymentService;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMessageBus _messageBus;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AppointmentOrchestrator(
            IPatientService patientService,
            IDoctorService doctorService,
            IScheduleService scheduleService,
            INotificationService notificationService,
            IPaymentService paymentService,
            IAppointmentRepository appointmentRepository,
            IMessageBus messageBus)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
            _doctorService = doctorService ?? throw new ArgumentNullException(nameof(doctorService));
            _scheduleService = scheduleService ?? throw new ArgumentNullException(nameof(scheduleService));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            _appointmentRepository = appointmentRepository ?? throw new ArgumentNullException(nameof(appointmentRepository));
            _messageBus = messageBus ?? throw new ArgumentNullException(nameof(messageBus));
        }

        public async Task<OrchestrationResult> CoordinateAppointmentCreation(AppointmentRequest request)
        {
            var result = new OrchestrationResult { Success = false };

            try
            {
                log.Info($"Iniciando orquestación de cita para paciente: {request.PatientId}");

                // PASO 1: Validaciones paralelas
                var patientTask = _patientService.GetPatientById(request.PatientId);
                var doctorTask = _doctorService.GetDoctorById(request.DoctorId);

                await Task.WhenAll(patientTask, doctorTask);

                var patient = await patientTask;
                var doctor = await doctorTask;

                if (patient == null)
                {
                    result.ErrorMessage = "Paciente no encontrado";
                    return result;
                }

                if (doctor == null)
                {
                    result.ErrorMessage = "Médico no encontrado";
                    return result;
                }

                // PASO 2: Verificar disponibilidad
                var isAvailable = await _scheduleService.CheckAvailability(
                    request.DoctorId,
                    request.AppointmentDate);

                if (!isAvailable)
                {
                    result.ErrorMessage = "El horario solicitado no está disponible";
                    return result;
                }

                // PASO 3: Procesar pago (si aplica)
                bool paymentProcessed = false;
                if (request.Payment != null && request.Payment.Amount > 0)
                {
                    paymentProcessed = await _paymentService.ProcessPayment(
                        request.PatientId,
                        request.Payment.Amount,
                        request.Payment.PaymentMethod);

                    if (!paymentProcessed)
                    {
                        result.ErrorMessage = "Error al procesar el pago";
                        return result;
                    }
                }

                // PASO 4: Crear la cita
                var appointment = new Appointment
                {
                    PatientId = request.PatientId,
                    DoctorId = request.DoctorId,
                    AppointmentDate = request.AppointmentDate,
                    Reason = request.Reason,
                    Status = "Confirmed",
                    CreatedAt = DateTime.Now
                };

                var savedAppointment = await _appointmentRepository.CreateAppointment(appointment);

                // PASO 5: Publicar evento
                await _messageBus.PublishAsync(new AppointmentCreatedEvent
                {
                    AppointmentId = savedAppointment.Id,
                    PatientId = request.PatientId,
                    DoctorId = request.DoctorId,
                    AppointmentDate = request.AppointmentDate,
                    Timestamp = DateTime.Now
                });

                // PASO 6: Enviar notificaciones (async)
                await _notificationService.SendAppointmentConfirmation(
                    patient.Email,
                    doctor.Name,
                    request.AppointmentDate);

                //_ = _notificationService.SendAppointmentConfirmation(
                //    patient.Email,
                //    doctor.Name,
                //    request.AppointmentDate);

                // PASO 7: Respuesta exitosa
                result.Success = true;
                result.AppointmentId = savedAppointment.Id;
                result.Confirmation = new AppointmentConfirmation
                {
                    AppointmentId = savedAppointment.Id,
                    ScheduledDate = savedAppointment.AppointmentDate,
                    DoctorName = doctor.Name,
                    PatientName = patient.Name,
                    NotificationSent = true,
                    PaymentProcessed = paymentProcessed
                };

                return result;
            }
            catch (Exception ex)
            {
                log.Error($"Error durante orquestación: {ex.Message}", ex);
                result.ErrorMessage = "Error interno al procesar la solicitud";
                return result;
            }
        }

        public async Task<OrchestrationResult> CoordinateAppointmentCancellation(int appointmentId)
        {
            var result = new OrchestrationResult { Success = false };

            try
            {
                var appointment = await _appointmentRepository.GetAppointmentById(appointmentId);

                if (appointment == null)
                {
                    result.ErrorMessage = "Cita no encontrada";
                    return result;
                }

                appointment.Status = "Cancelled";
                appointment.UpdatedAt = DateTime.Now;

                await _appointmentRepository.UpdateAppointment(appointment);
                await _scheduleService.ReleaseTimeSlot(appointment.DoctorId, appointment.AppointmentDate);

                await _messageBus.PublishAsync(new AppointmentCancelledEvent
                {
                    AppointmentId = appointmentId,
                    CancelledAt = DateTime.Now
                });

                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                log .Error($"Error al cancelar cita: {ex.Message}", ex);
                result.ErrorMessage = "Error interno al cancelar la cita";
                return result;
            }
        }
    }
}