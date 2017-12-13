using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class Doctor
    {
        public Guid DoctorId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Speciality { get; private set; }
        public string Hospital { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
        public List<Appointment> Appointments { get; private set; }
        public List<Feedback> Feedbacks { get; private set; }

        private Doctor() { }

        public static Doctor Create(string firstName, string lastName, string email, string password, string phoneNumber, string speciality, string hospital, string city, string address)
        {
            var instance = new Doctor { DoctorId = Guid.NewGuid(), Appointments = new List<Appointment>(), Feedbacks = new List<Feedback>() };
            instance.Update(firstName, lastName, email, password, phoneNumber, speciality, hospital, city, address, null, null);
            return instance;
        }

        public void Update(string firstName, string lastName, string email, string password, string phoneNumber, string speciality, string hospital, string city, string address, List<Appointment> appointments, List<Feedback> feedbacks)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            Speciality = speciality;
            Hospital = hospital;
            City = city;
            Address = address;
            if (appointments != null)
            {
                foreach (var appointment in appointments)
                {
                    Appointments.Add(appointment);
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
