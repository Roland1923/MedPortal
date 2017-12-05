using System;
using System.ComponentModel.DataAnnotations;
using Core.CustomAnnotations;

namespace Core.Entities
{
    public class Patient
    {
        private Patient () { }

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


        public static Patient Create(string firstName, string lastName, string email, string password, string city, DateTime birthdate, string phoneNumber)
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
