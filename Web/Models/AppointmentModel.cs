using System;
using Core.Entities;

namespace Web.Models
{
    public class AppointmentModel
    {
        public Guid AppointmentId { get;  set; }
        public DateTime AppointmentDate { get;  set; }
        //public List<Doctor> DoctorForAppointment { get; private set; }
        public Patient PatientForAppointment { get;  set; }
    }
}
