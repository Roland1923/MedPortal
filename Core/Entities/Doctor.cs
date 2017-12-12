using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Doctor
    {
        [Key]
        [Required]
        public Guid DoctorId { get; private set; }

        [Required(ErrorMessage = "You must provide your FirstName!")]
        [Display(Name = "FirstName")]
        [MinLength(3), MaxLength(30)]
        public string FirstName { get; private set; }

        [Required(ErrorMessage = "You must provide your LastName!")]
        [Display(Name = "LastName")]
        [MinLength(3), MaxLength(30)]
        public string LastName { get; private set; }

        [Required(ErrorMessage = "You must provide a PhoneNumber!")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; private set; }

        [Required(ErrorMessage = "You must provide a password!")]
        [Display(Name = "Password")]
        [StringLength(18, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; private set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid PhoneNumber!")]
        public string PhoneNumber { get; private set; }

        [Required(ErrorMessage = "You must provide your speciality!")]
        [Display(Name = "Speciality")]
        [DataType(DataType.Text)]
        public string Speciality { get; private set; }

        [Required(ErrorMessage = "You must provide a hospital!")]
        [Display(Name = "Hospital")]
        [DataType(DataType.Text)]
        public string Hospital { get; private set; }

        [Required(ErrorMessage = "You must provide a City!")]
        [Display(Name = "City")]
        public string City { get; private set; }

        [Required(ErrorMessage = "You must provide an address!")]
        [Display(Name = "Address")]
        [DataType(DataType.Text)]
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
