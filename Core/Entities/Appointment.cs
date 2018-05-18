using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Appointment
    {
        [Key]
        public Guid AppointmentId { get; private set; }
        public Guid PatientId { get; private set; }
        [ForeignKey("PatientId")]
        public Patient Patient => null;
        public Guid DoctorId { get; private set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor => null;
        public DateTime AppointmentDate { get; private set; }

        private Appointment() { }

        public static Appointment Create(DateTime appointmentDate, Guid doctorId, Guid patientId)
        {
            var instance = new Appointment { AppointmentId = Guid.NewGuid()};
            instance.Update(appointmentDate, doctorId, patientId);
            return instance;
        }

        public void Update(DateTime appointmentDate, Guid doctorId, Guid patientId)
        {
            AppointmentDate = appointmentDate;
            DoctorId = doctorId;
            PatientId = patientId;
        }
    }
}
