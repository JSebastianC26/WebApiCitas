-- =============================================
-- VISTAS
-- =============================================
CREATE VIEW vw_AppointmentDetails AS
SELECT 
    a.Id AS AppointmentId,
    a.AppointmentDate,
    a.Status,
    a.Reason,
    p.Id AS PatientId,
    p.Name AS PatientName,
    p.Email AS PatientEmail,
    p.Phone AS PatientPhone,
    d.Id AS DoctorId,
    d.Name AS DoctorName,
    d.Specialty AS DoctorSpecialty,
    d.Email AS DoctorEmail,
    pay.Amount AS PaymentAmount,
    pay.Status AS PaymentStatus
FROM Appointments a
INNER JOIN Patients p ON a.PatientId = p.Id
INNER JOIN Doctors d ON a.DoctorId = d.Id
LEFT JOIN Payments pay ON a.Id = pay.AppointmentId;

CREATE VIEW vw_DoctorStatistics AS
SELECT 
    d.Id AS DoctorId,
    d.Name AS DoctorName,
    d.Specialty,
    COUNT(a.Id) AS TotalAppointments,
    SUM(CASE WHEN a.Status = 'Completed' THEN 1 ELSE 0 END) AS CompletedAppointments,
    SUM(CASE WHEN a.Status = 'Cancelled' THEN 1 ELSE 0 END) AS CancelledAppointments,
    SUM(CASE WHEN a.Status = 'NoShow' THEN 1 ELSE 0 END) AS NoShowAppointments
FROM Doctors d
LEFT JOIN Appointments a ON d.Id = a.DoctorId
GROUP BY d.Id, d.Name, d.Specialty;