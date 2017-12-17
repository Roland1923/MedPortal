using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Appointment
    {
        [Key]
        public Guid AppointmentId { get; private set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; private set; }
        public Guid PatientId { get; private set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; private set; }
        public Guid DoctorId { get; private set; }
        public DateTime AppointmentDate { get; private set; }

        private Appointment() { }

        public static Appointment Create(DateTime appointmentDate, Doctor doctor, Patient patient)
        {
            var instance = new Appointment { AppointmentId = Guid.NewGuid()};
            instance.Update(appointmentDate, doctor, patient);
            return instance;
        }

        public void Update(DateTime appointmentDate, Doctor doctor, Patient patient)
        {
            AppointmentDate = appointmentDate;
            Doctor = doctor;
            Patient = patient;
        }
    }
}
