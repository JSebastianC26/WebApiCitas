using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApiCitas.Interfaces;
using WebApiCitas.Repositorios;

namespace WebApiCitas.Services
{
    /// <summary>
    /// Configuración de Inyección de Dependencias usando Autofac
    /// </summary>
    public static class DependencyConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            // ===== REGISTRAR CONTROLADORES =====
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

            // ===== CONFIGURACIÓN DE CONEXIÓN =====
            var connectionString = ConfigurationManager
                .ConnectionStrings["MedicalAppointmentsDB"].ConnectionString;

            // ===== REGISTRAR REPOSITORIOS =====
            builder.RegisterType<AppointmentRepository>()
                .As<IAppointmentRepository>()
                .WithParameter("connectionString", connectionString)
                .InstancePerRequest();

            // ===== REGISTRAR SERVICIOS =====
            builder.RegisterType<PatientService>()
                .As<IPatientService>()
                .WithParameter("connectionString", connectionString)
                .InstancePerRequest();

            builder.RegisterType<DoctorService>()
                .As<IDoctorService>()
                .WithParameter("connectionString", connectionString)
                .InstancePerRequest();

            builder.RegisterType<ScheduleService>()
                .As<IScheduleService>()
                .WithParameter("connectionString", connectionString)
                .InstancePerRequest();

            builder.RegisterType<NotificationService>()
                .As<INotificationService>()
                .InstancePerRequest();

            builder.RegisterType<PaymentService>()
                .As<IPaymentService>()
                .WithParameter("connectionString", connectionString)
                .InstancePerRequest();

            // ===== REGISTRAR MESSAGE BUS =====
            var rabbitMQHost = ConfigurationManager.AppSettings["RabbitMQ:Host"] ?? "localhost";

            builder.RegisterType<RabbitMQMessageBus>()
                .As<IMessageBus>()
                .WithParameter("hostName", rabbitMQHost)
                .SingleInstance();

            // ===== REGISTRAR ORQUESTADOR =====
            builder.RegisterType<AppointmentOrchestrator>()
                .As<IAppointmentOrchestrator>()
                .InstancePerRequest();

            // ===== REGISTRAR UTILIDADES =====
            builder.RegisterType<SmtpEmailSender>()
                .As<IEmailSender>()
                .InstancePerRequest();

            // ===== CONSTRUIR CONTENEDOR =====
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }

}