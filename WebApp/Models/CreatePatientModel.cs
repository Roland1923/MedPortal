using System;
using Core.Entities;

namespace WebApp.Models
{
    public class CreatePatientModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public DateTime Birthdate { get; set; }
        public string PhoneNumber { get; set; }
        //public BloodDonor BloodDonor { get; set; }
    }
}
