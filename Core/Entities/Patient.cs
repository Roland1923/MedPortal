using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.CustomAnnotations;

namespace Core.Entities
{
    public class Patient
    {
        [Key]
        [Required]
        public Guid PatientId { get; private set; }

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

        [Required(ErrorMessage = "You must provide a City!")]
        [Display(Name = "City")]
        public string City { get; private set; }

        [Required(ErrorMessage = "You must provide a Birthdate!")]
        [Display(Name = "Birthdate")]
        [MyDate]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; private set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid PhoneNumber!")]
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
