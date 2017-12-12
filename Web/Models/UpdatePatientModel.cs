using System;
using System.Collections.Generic;
using Core.Entities;

namespace Web.Models
{
    public class UpdatePatientModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public DateTime Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
