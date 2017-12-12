using System.Collections.Generic;
using Core.Entities;

namespace WebApp.Models
{
    public class UpdateDoctorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Speciality { get; set; }
        public string Hospital { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Feedback> Feedbacks { get; set; }
    }
}
