using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Patient
    {
        public Guid PatientId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string City { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string PhoneNumber { get; private set; }
        [ForeignKey("BloodDonorId")]
        public BloodDonor BloodDonor { get; private set; }
        public Guid? BloodDonorId { get; private set; }
        public List<Appointment> Appointments { get; private set; }
        public List<PatientHistory> PatientHistories { get; private set; }
        public List<Feedback> Feedbacks { get; private set; }

        private Patient() { }

        public static Patient Create(string firstName, string lastName, string email, string password, string city, DateTime birthdate, string phoneNumber, BloodDonor bloodDonor)
        {
            var instance = new Patient { PatientId = Guid.NewGuid(), Appointments = new List<Appointment>(), PatientHistories = new List<PatientHistory>(), Feedbacks = new List<Feedback>() };
            instance.Update(firstName, lastName, email, password, city, birthdate, phoneNumber, bloodDonor, null, null, null);
            return instance;
        }

        public void Update(string firstName, string lastName, string email, string password, string city, DateTime birthdate, string phoneNumber, BloodDonor bloodDonor, List<Appointment> appointments, List<PatientHistory> patientHistories, List<Feedback> feedbacks)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            City = city;
            Birthdate = birthdate;
            PhoneNumber = phoneNumber;
            BloodDonor = bloodDonor;
            if (appointments != null)
            {
                foreach (var appointment in appointments)
                {
                    Appointments.Add(appointment);
                }
            }
            if (patientHistories != null)
            {
                foreach (var patientHistory in patientHistories)
                {
                    PatientHistories.Add(patientHistory);
                }
            }
            if (feedbacks != null)
            {
                foreach (var feedback in feedbacks)
                {
                    Feedbacks.Add(feedback);
                }
            }
        }
    }
}
