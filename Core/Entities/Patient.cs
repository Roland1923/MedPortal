using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Patient
    {
        [Key]
        public Guid PatientId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string City { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string PhoneNumber { get; private set; }
        [ForeignKey("BloodDonorId")]
        public BloodDonor BloodDonor => null;
        public Guid? BloodDonorId => null;
        public List<Appointment> Appointments => null;
        public List<PatientHistory> PatientHistories => null;
        public List<Feedback> Feedbacks => null;

        private Patient() { }

        public static Patient Create(string firstName, string lastName, string email, string password, string city, DateTime birthdate, string phoneNumber, BloodDonor bloodDonor)
        {
            var instance = new Patient { PatientId = Guid.NewGuid() };
            instance.Update(firstName, lastName, email, password, city, birthdate, phoneNumber);
            return instance;
        }

        public void Update(string firstName, string lastName, string email, string password, string city, DateTime birthdate, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            City = city;
            Birthdate = birthdate;
            PhoneNumber = phoneNumber;
        }
    }
}
