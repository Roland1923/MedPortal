using System;
using System.ComponentModel.DataAnnotations;
using Core.CustomAnnotations;

namespace Core.Entities
{
    public class Appointment
    {
        private Appointment() { }

        [Key]
        [Required]
        public Guid AppointmentId { get; private set; }

        public Patient PatientForAppointment { get; private set; }
        
        
        [Required(ErrorMessage = "You must provide an Appointment Date")]
        [Display(Name = "AppointmentDate")]
        [MyDate]
        [DataType(DataType.Date)]
        public DateTime AppointmentDate { get; private set; }

        public static Appointment Create(DateTime appointmentDate,Patient patient)
        {
            var instance = new Appointment() { AppointmentId = Guid.NewGuid()};
            instance.Update(appointmentDate,patient);
            return instance;
        }

        public void Update(DateTime appointmentDate,Patient patient)
        {
            AppointmentDate = appointmentDate;
            PatientForAppointment = patient;
        }
    }
}
