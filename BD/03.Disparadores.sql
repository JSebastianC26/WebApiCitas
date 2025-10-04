-- =============================================
-- AUDITRORIAS
-- =============================================
DELIMITER //

CREATE TRIGGER tr_Appointments_Audit
AFTER INSERT ON Appointments
FOR EACH ROW
BEGIN
    INSERT INTO AuditLog (EntityType, EntityId, Action)
    VALUES ('Appointment', NEW.Id, 'INSERT');
END;
//

CREATE TRIGGER tr_Appointments_Audit_Update
AFTER UPDATE ON Appointments
FOR EACH ROW
BEGIN
    IF OLD.Status <> NEW.Status THEN
        INSERT INTO AuditLog (EntityType, EntityId, Action, Changes)
        VALUES ('Appointment', NEW.Id, 'UPDATE',
                CONCAT('Status changed from ', OLD.Status, ' to ', NEW.Status));
    END IF;
END;
//

CREATE TRIGGER tr_Appointments_Audit_Delete
AFTER DELETE ON Appointments
FOR EACH ROW
BEGIN
    INSERT INTO AuditLog (EntityType, EntityId, Action)
    VALUES ('Appointment', OLD.Id, 'DELETE');
END;
//

CREATE TRIGGER tr_hash_password
BEFORE INSERT ON ApiUsers
FOR EACH ROW
BEGIN
    SET NEW.ClaveHash = SHA2(NEW.ClaveHash, 256);
END;
//

CREATE TRIGGER tr_hash_password_update
BEFORE UPDATE ON ApiUsers
FOR EACH ROW
BEGIN
    IF OLD.ClaveHash <> NEW.ClaveHash THEN
        SET NEW.ClaveHash = SHA2(NEW.ClaveHash, 256);
    END IF;
END;
//
DELIMITER ;