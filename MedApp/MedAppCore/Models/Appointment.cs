using iText.IO.Util;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MedAppCore.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
    }

    public class AppointmentResource
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime DateTime { get; set; }
        public string Status { get; set; }
    }

    public enum AppointmentStatus { 
        Waiting,
        InProgress,
        Finished
    }

    public class AppointmentBase
    {
        [BsonId]
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime DateTime { get; set; }
        public AppointmentStatus Status { get; set; }
    }

    public class AppointmentBaseResource
    {
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime DateTime { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
