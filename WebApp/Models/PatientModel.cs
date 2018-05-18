using System;

namespace WebApp.Models
{
    public class PatientModel
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
