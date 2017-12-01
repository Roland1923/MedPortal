using System;

namespace Web.Models
{
    public class PatientModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public DateTime Birthdate { get; set; }
        public string PhoneNumber { get; set; }
    }
}
