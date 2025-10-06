# Sistema de Gestión de Citas Médicas 🏥

Sistema de gestión de citas médicas implementado con **Arquitectura Orientada a Servicios (SOA)** utilizando el patrón de **Orquestación** para coordinar múltiples servicios independientes.

[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.6.2-blue)](https://dotnet.microsoft.com/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)
[![Status](https://img.shields.io/badge/status-active-success)]()

---

## 📋 Tabla de Contenidos

- [Descripción](#descripción)
- [Arquitectura](#arquitectura)
- [Características](#características)
- [Tecnologías](#tecnologías)
- [Requisitos Previos](#requisitos-previos)
- [Instalación](#instalación)
- [Configuración](#configuración)
- [Uso](#uso)
- [API Endpoints](#api-endpoints)
- [Casos de Prueba](#casos-de-prueba)
- [Estructura del Proyecto](#estructura-del-proyecto)
- [Documentación](#documentación)
- [Contribución](#contribución)
- [Licencia](#licencia)
- [Autor](#autor)

---

## 📖 Descripción

Este proyecto implementa un sistema completo de gestión de citas médicas que permite a pacientes agendar citas con médicos, procesar pagos, enviar notificaciones y gestionar la disponibilidad de horarios. Utiliza el patrón de **Orquestación SOA** donde un coordinador central (`AppointmentCoordinator`) gestiona el flujo completo de creación de citas, invocando múltiples servicios independientes.

### Objetivo Académico

Este proyecto fue desarrollado como parte de un trabajo académico sobre arquitecturas de software, específicamente para demostrar la implementación práctica del patrón SOA con orquestación en un contexto de salud digital.

---

## 🏗️ Arquitectura

### Diagrama de Arquitectura Lógica

```
┌─────────────────────────────────────────────────────┐
│              Clientes (Web, Móvil)                   │
└────────────────────┬────────────────────────────────┘
                     │
                     ▼
┌──────────────────────────────────────────────────────┐
│         API Gateway (Punto de Entrada)                │
└────────────────────┬─────────────────────────────────┘
                     │
                     ▼
┌──────────────────────────────────────────────────────┐
│      AppointmentCoordinator (Orquestador)            │
│      - Coordina el flujo de creación de citas        │
└──┬────┬────┬────┬────┬────┬─────────────────────────┘
   │    │    │    │    │    │
   ▼    ▼    ▼    ▼    ▼    ▼
┌────┐┌────┐┌────┐┌────┐┌────┐
│Pat ││Doc ││Sch ││Pay ││Not │
│Svc ││Svc ││Svc ││Svc ││Svc │
└────┘└────┘└────┘└────┘└────┘
   │    │    │    │    │
   └────┴────┴────┴────┴──────┐
                               ▼
                    ┌─────────────────┐
                    │   MY SQL        │
                    └─────────────────┘
                               │
                               ▼
                    ┌─────────────────┐
                    │   Message Bus   │
                    │   (RabbitMQ)    │
                    └─────────────────┘
```

### Componentes Principales

- **AppointmentCoordinator**: Orquestador central que coordina el proceso completo
- **PatientService**: Gestión de pacientes
- **DoctorService**: Gestión de médicos y especialidades
- **ScheduleService**: Control de disponibilidad y agenda
- **PaymentService**: Procesamiento de pagos
- **NotificationService**: Envío de notificaciones (email/SMS)
- **Message Bus**: Sistema de mensajería asíncrona (RabbitMQ)

---

## ✨ Características

- **Patrón SOA con Orquestación**: Coordinación centralizada de servicios
- **Servicios independientes**: Cada servicio tiene responsabilidad única
- **Comunicación asíncrona**: Message bus para eventos desacoplados
- **Validaciones robustas**: Verificación de datos en múltiples niveles
- **Manejo de errores**: Try-catch centralizado con logging exhaustivo
- **Transacciones atómicas**: Rollback automático en caso de fallos
- **Prevención de doble reserva**: Control de concurrencia en horarios
- **Procesamiento de pagos**: Integración con pasarelas de pago
- **Notificaciones automáticas**: Confirmaciones por email
- **Logging detallado**: Trazabilidad completa de operaciones
- **Inyección de dependencias**: Bajo acoplamiento con Autofac
- **API RESTful**: Endpoints estándar con documentación

---

## 🛠️ Tecnologías

### Backend
- **Framework**: ASP.NET Web API (.NET Framework 4.6.2)
- **Lenguaje**: C# 7.3
- **Base de Datos**: MY SQL
- **ORM**: Dapper (micro-ORM de alto rendimiento)
- **DI Container**: Autofac 6.5.0
- **Serialización**: Newtonsoft.Json 13.0.3

### Infraestructura
- **Message Bus**: RabbitMQ 6.5.0
- **Servidor Web**: IIS 10
- **Testing**: Postman

### Patrones de Diseño
- Service-Oriented Architecture (SOA)
- Orchestration Pattern
- Repository Pattern
- Dependency Injection
- Async/Await Pattern

---
## ⚙️ Configuración

### Variables de Configuración (Web.config)

```xml
<appSettings>
  <!-- Message Bus -->
  <add key="RabbitMQ:Host" value="localhost" />
  <add key="RabbitMQ:Port" value="5672" />
  
  <!-- Email (SMTP) -->
  <add key="SMTP:Host" value="smtp.gmail.com" />
  <add key="SMTP:Port" value="587" />
  <add key="SMTP:User" value="tu-email@gmail.com" />
  <add key="SMTP:Password" value="tu-app-password" />
</appSettings>
```

### Configuración de Servicios

El proyecto usa **Autofac** para inyección de dependencias. La configuración está en:

```
App_Start/DependencyConfig.cs
```
---

## 💻 Uso

### Crear una Cita Médica

**Endpoint:** `POST /api/appointmentCoordinator`

**Request:**
```json
{
  "patientId": 1,
  "doctorId": 2,
  "appointmentDate": "2025-11-01T10:30:00",
  "reason": "Consulta general",
  "payment": {
    "amount": 85000,
    "paymentMethod": "Credit Card"
  }
}
```

**Response (200 OK):**
```json
{
  "success": true,
  "appointmentId": 15,
  "message": "Cita agendada exitosamente",
  "data": {
    "appointmentId": 15,
    "scheduledDate": "2025-11-01T10:30:00",
    "doctorName": "Dr. Roberto Martínez",
    "patientName": "Juan Pérez",
    "notificationSent": true,
    "paymentProcessed": true
  }
}
```

### Cancelar una Cita

**Endpoint:** `PUT /api/appointments/cancel/{id}`

```bash
curl -X PUT http://localhost:44385/api/appointments/cancel/15
```

---

## 🔌 API Endpoints

### Orquestador de Citas

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `/api/appointmentCoordinator` | Crear nueva cita |

### Pacientes

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/patients/{id}` | Obtener paciente por ID |
| POST | `/api/patients` | Registrar nuevo paciente |

### Médicos

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/doctors/{id}` | Obtener médico por ID |
| GET | `/api/doctors?specialty=X` | Listar médicos por especialidad |

### Agenda

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| GET | `/api/appointments/availability?doctorId=X&date=Y` | Consultar disponibilidad |
| POST | `/api/appointments/schedule` | Reservar horario |

### Cancelación

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| PUT | `/api/appointments/cancel/{id}` | Cancelar cita |

### Notificaciones

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `/api/notifications/send` | Enviar notificación manual |

### Pagos

| Método | Endpoint | Descripción |
|--------|----------|-------------|
| POST | `/api/payments/confirm` | Confirmar pago |

**Documentación completa:** Importar la colección de Postman en `/Postman/MedicalAppointments.postman_collection.json`

---

## 🧪 Casos de Prueba

```

### Casos de Prueba Principales

1. **CP-001**: Crear cita exitosamente
2. **CP-002**: Crear cita con pago
3. **CP-003**: Error - Paciente no encontrado
4. **CP-004**: Error - Horario no disponible
5. **CP-005**: Error - Pago rechazado
6. **CP-006**: Cancelar cita exitosamente
7. **CP-007**: Validación de campos requeridos

Ver documentación completa en: `/Docs/CasosDePrueba.md`

---

### Guía de Estilo

- Seguir convenciones de C# estándar
- Documentar métodos públicos con XML comments
- Escribir pruebas unitarias para nuevas funcionalidades
- Mantener cobertura de código > 80%

---

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.

---

## 👤 Autor (es)

**SubGrupo 11**

- GitHub: [@JSebastianC26](https://github.com/JSebastianC26)
- LinkedIn: [johan-sebastian-cardona-figueroa](https://www.linkedin.com/in/johan-sebastian-cardona-figueroa-a42b131ab)
- Email: jscardona026@gmai.com

---

## 🙏 Agradecimientos

- Universidad [Politecnico GranColombiano] - Arquitectura de Software
- Profesor [Natalia Martinez] - Asesoría académica
- Comunidad ASP.NET por recursos y documentación


**⭐ Si este proyecto te fue útil, considera darle una estrella en GitHub**

---

*Última actualización: Octubre 2025*
