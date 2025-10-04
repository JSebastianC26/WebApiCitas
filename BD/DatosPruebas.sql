-- =============================================
-- DATOS DE PRUEBA
-- =============================================
-- Pacientes
INSERT INTO Patients (Name, Email, Phone, DateOfBirth, Address) VALUES
('Juan Pérez', 'juan.perez@email.com', '3001234567', '1985-05-15', 'Calle 123 #45-67'),
('María González', 'maria.gonzalez@email.com', '3009876543', '1990-08-22', 'Carrera 45 #12-34'),
('Carlos Rodríguez', 'carlos.rodriguez@email.com', '3005551234', '1978-12-10', 'Avenida 68 #23-45');

-- Doctores
INSERT INTO Doctors (Name, Specialty, Email, Phone, LicenseNumber) VALUES
('Dr. Roberto Martínez', 'Medicina General', 'dr.martinez@hospital.com', '3101234567', 'MED-2020-001'),
('Dra. Ana López', 'Cardiología', 'dra.lopez@hospital.com', '3109876543', 'MED-2019-045'),
('Dr. Luis Hernández', 'Pediatría', 'dr.hernandez@hospital.com', '3105551234', 'MED-2021-089');

-- Citas
INSERT INTO Appointments (PatientId, DoctorId, AppointmentDate, Reason, Status) VALUES
(1, 1, DATE_ADD(NOW(), INTERVAL 1 DAY), 'Chequeo general', 'Confirmed'),
(2, 2, DATE_ADD(NOW(), INTERVAL 2 DAY), 'Control cardiológico', 'Pending'),
(3, 3, DATE_ADD(NOW(), INTERVAL 3 DAY), 'Consulta pediátrica', 'Confirmed');

-- Usuarios api
INSERT INTO ApiUsers (Username, ClaveHash, IsActive) VALUES
('SWCITASWEB', '123456', TRUE);