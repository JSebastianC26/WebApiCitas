-- =============================================
-- PROCEDIMIENTOS
-- =============================================
DELIMITER //

CREATE PROCEDURE sp_GetDoctorAvailability(IN p_DoctorId INT, IN p_Date DATE)
BEGIN
    SELECT 
        AppointmentDate,
        Status
    FROM Appointments
    WHERE DoctorId = p_DoctorId
      AND DATE(AppointmentDate) = p_Date
      AND Status <> 'Cancelled'
    ORDER BY AppointmentDate;
END;
//

CREATE PROCEDURE sp_GetPatientAppointments(IN p_PatientId INT, IN p_IncludePast BOOLEAN)
BEGIN
    SELECT 
        a.Id,
        a.AppointmentDate,
        a.Reason,
        a.Status,
        d.Name AS DoctorName,
        d.Specialty
    FROM Appointments a
    INNER JOIN Doctors d ON a.DoctorId = d.Id
    WHERE a.PatientId = p_PatientId
      AND (p_IncludePast = 1 OR a.AppointmentDate >= NOW())
    ORDER BY a.AppointmentDate DESC;
END;
//

CREATE PROCEDURE sp_CleanExpiredReservations()
BEGIN
    DELETE FROM TimeSlotReservations
    WHERE ExpiresAt < NOW();
    
    SELECT ROW_COUNT() AS DeletedRows;
END;
//

DELIMITER ;