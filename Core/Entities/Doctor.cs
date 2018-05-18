using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Doctor
    {
        [Key]
        public Guid DoctorId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Description { get; private set; }
        public string Speciality { get; private set; }
        public string Hospital { get; private set; }
        public string City { get; private set; }
        public string Address { get; private set; }
        public List<Appointment> Appointments => null;
        public List<Feedback> Feedbacks => null;

        private Doctor() { }

        public static Doctor Create(string firstName, string lastName, string email, string password, string phoneNumber, string description, string speciality, string hospital, string city, string address)
        {
            var instance = new Doctor { DoctorId = Guid.NewGuid() };
            instance.Update(firstName, lastName, email, password, phoneNumber, description, speciality, hospital, city, address);
            return instance;
        }

        public void Update(string firstName, string lastName, string email, string password, string phoneNumber, string description, string speciality, string hospital, string city, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            Description = description;
            Speciality = speciality;
            Hospital = hospital;
            City = city;
            Address = address;
        }
    }
}
