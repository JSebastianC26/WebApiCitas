using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiCitas.Interfaces;
using WebApiCitas.Models;

namespace WebApiCitas.Repositorios
{
    public class ScheduleService : IScheduleService
    {
        private readonly string _connectionString;
        private readonly IAppointmentRepository _appointmentRepository;

        public ScheduleService(
            string connectionString,
            IAppointmentRepository appointmentRepository)
        {
            _connectionString = connectionString;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<bool> CheckAvailability(int doctorId, DateTime date)
        {
            // Obtener citas existentes del médico en esa fecha
            var existingAppointments = await _appointmentRepository
                .GetAppointmentsByDoctor(doctorId, date);

            // Verificar si hay conflicto de horario
            var hasConflict = existingAppointments.Any(a =>
                Math.Abs((a.AppointmentDate - date).TotalMinutes) < 30);

            return !hasConflict;
        }

        public async Task<bool> ReserveTimeSlot(int doctorId, DateTime date)
        {
            // Implementar lógica de reserva temporal
            // Puede usar una tabla temporal o cache
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    INSERT INTO TimeSlotReservations 
                        (DoctorId, ReservedDate, ReservedAt, ExpiresAt)
                    VALUES 
                        (@DoctorId, @ReservedDate, GETDATE(), DATEADD(MINUTE, 15, GETDATE()))";

                await connection.ExecuteAsync(sql, new { DoctorId = doctorId, ReservedDate = date });
                return true;
            }
        }

        public async Task ReleaseTimeSlot(int doctorId, DateTime date)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                    DELETE FROM TimeSlotReservations 
                    WHERE DoctorId = @DoctorId AND ReservedDate = @ReservedDate";

                await connection.ExecuteAsync(sql, new { DoctorId = doctorId, ReservedDate = date });
            }
        }

        public async Task<List<TimeSlot>> GetAvailableSlots(int doctorId, DateTime date)
        {
            var slots = new List<TimeSlot>();
            var startHour = 8; // 8 AM
            var endHour = 17; // 5 PM

            // Obtener citas existentes
            var existingAppointments = await _appointmentRepository
                .GetAppointmentsByDoctor(doctorId, date);

            // Generar slots de 30 minutos
            for (int hour = startHour; hour < endHour; hour++)
            {
                for (int minute = 0; minute < 60; minute += 30)
                {
                    var slotTime = new DateTime(
                        date.Year, date.Month, date.Day,
                        hour, minute, 0);

                    var isOccupied = existingAppointments.Any(a =>
                        Math.Abs((a.AppointmentDate - slotTime).TotalMinutes) < 30);

                    slots.Add(new TimeSlot
                    {
                        StartTime = slotTime,
                        EndTime = slotTime.AddMinutes(30),
                        IsAvailable = !isOccupied
                    });
                }
            }

            return slots.Where(s => s.IsAvailable).ToList();
        }
    }
}