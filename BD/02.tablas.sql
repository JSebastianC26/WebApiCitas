-- =============================================
-- TABLA: Patients
-- =============================================
CREATE TABLE Patients (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Phone VARCHAR(20),
    DateOfBirth DATE,
    Address VARCHAR(200),
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE INDEX IX_Patients_Email ON Patients(Email);

-- =============================================
-- TABLA: Doctors
-- =============================================
CREATE TABLE Doctors (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Specialty VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Phone VARCHAR(20),
    LicenseNumber VARCHAR(50) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE INDEX IX_Doctors_Specialty ON Doctors(Specialty);

-- =============================================
-- TABLA: Appointments
-- =============================================
CREATE TABLE Appointments (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    PatientId INT NOT NULL,
    DoctorId INT NOT NULL,
    AppointmentDate DATETIME NOT NULL,
    Reason VARCHAR(500),
    Status VARCHAR(50) NOT NULL DEFAULT 'Pending',
    Notes TEXT,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,
    CancelledAt DATETIME NULL,
    CancellationReason VARCHAR(500),
    
    CONSTRAINT FK_Appointments_Patient FOREIGN KEY (PatientId) 
        REFERENCES Patients(Id),
    CONSTRAINT FK_Appointments_Doctor FOREIGN KEY (DoctorId) 
        REFERENCES Doctors(Id),
    CONSTRAINT CK_Appointments_Status CHECK (Status IN 
        ('Pending', 'Confirmed', 'Completed', 'Cancelled', 'NoShow'))
);

CREATE INDEX IX_Appointments_PatientId ON Appointments(PatientId);
CREATE INDEX IX_Appointments_DoctorId ON Appointments(DoctorId);
CREATE INDEX IX_Appointments_Date ON Appointments(AppointmentDate);
CREATE INDEX IX_Appointments_Status ON Appointments(Status);

-- =============================================
-- TABLA: TimeSlotReservations
-- =============================================
CREATE TABLE TimeSlotReservations (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    DoctorId INT NOT NULL,
    ReservedDate DATETIME NOT NULL,
    ReservedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ExpiresAt DATETIME NOT NULL,
    
    CONSTRAINT FK_TimeSlot_Doctor FOREIGN KEY (DoctorId) 
        REFERENCES Doctors(Id)
);

CREATE INDEX IX_TimeSlot_Doctor_Date ON TimeSlotReservations(DoctorId, ReservedDate);

-- =============================================
-- TABLA: Payments
-- =============================================
CREATE TABLE Payments (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    AppointmentId INT NULL,
    PatientId INT NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    PaymentMethod VARCHAR(50) NOT NULL,
    TransactionId VARCHAR(100),
    Status VARCHAR(50) NOT NULL DEFAULT 'Pending',
    ProcessedAt DATETIME NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    
    CONSTRAINT FK_Payments_Appointment FOREIGN KEY (AppointmentId) 
        REFERENCES Appointments(Id),
    CONSTRAINT FK_Payments_Patient FOREIGN KEY (PatientId) 
        REFERENCES Patients(Id),
    CONSTRAINT CK_Payments_Status CHECK (Status IN 
        ('Pending', 'Completed', 'Failed', 'Refunded'))
);

CREATE INDEX IX_Payments_AppointmentId ON Payments(AppointmentId);
CREATE INDEX IX_Payments_PatientId ON Payments(PatientId);

-- =============================================
-- TABLA: Notifications
-- =============================================
CREATE TABLE Notifications (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    AppointmentId INT NULL,
    RecipientEmail VARCHAR(100) NOT NULL,
    Subject VARCHAR(200) NOT NULL,
    Body TEXT NOT NULL,
    NotificationType VARCHAR(50) NOT NULL,
    Status VARCHAR(50) NOT NULL DEFAULT 'Pending',
    SentAt DATETIME NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    ErrorMessage TEXT,
    
    CONSTRAINT FK_Notifications_Appointment FOREIGN KEY (AppointmentId) 
        REFERENCES Appointments(Id),
    CONSTRAINT CK_Notifications_Type CHECK (NotificationType IN 
        ('Confirmation', 'Reminder', 'Cancellation', 'Custom')),
    CONSTRAINT CK_Notifications_Status CHECK (Status IN 
        ('Pending', 'Sent', 'Failed'))
);

CREATE INDEX IX_Notifications_AppointmentId ON Notifications(AppointmentId);
CREATE INDEX IX_Notifications_Status ON Notifications(Status);

-- =============================================
-- TABLA: AuditLog
-- =============================================
CREATE TABLE AuditLog (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    EntityType VARCHAR(50) NOT NULL,
    EntityId INT NOT NULL,
    Action VARCHAR(50) NOT NULL,
    UserId INT NULL,
    Changes TEXT,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    IpAddress VARCHAR(50)
);

CREATE INDEX IX_AuditLog_EntityType ON AuditLog(EntityType, EntityId);
CREATE INDEX IX_AuditLog_CreatedAt ON AuditLog(CreatedAt);


-- =============================================
-- TABLA: ApiUsers
-- =============================================
CREATE TABLE ApiUsers (
	Username VARCHAR(30) PRIMARY KEY,
    ClaveHash VARCHAR(100) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NULL ON UPDATE CURRENT_TIMESTAMP,
    IsActive BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE INDEX IX_ApiUsers_IsActive ON ApiUsers(IsActive);