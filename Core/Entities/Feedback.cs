using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class Feedback
    {
        private Feedback() { }

        [Key]
        [Required]
        public Guid FeedbackId { get; private set; }

        [Required(ErrorMessage = "You must provide a description!")]
        [Display(Name = "Feedback")]
        [MinLength(4), MaxLength(400)]
        public string Description { get; private set; }

        [Required(ErrorMessage = "You must provide a pacient id!")]
        [Display(Name = "PacientID")]
        public Patient CurrentPacient { get; private set; }

        [Required(ErrorMessage = "You must provide a doctor id!")]
        [Display(Name = "DoctorID")]
        public Doctor CurrentDoctor { get; private set; }

        [Required(ErrorMessage = "You must give a rating!")]
        [Display(Name = "Rating")]
        public string Rating { get; private set; }

        public static Feedback Create(string description, Patient patient, Doctor doctor, string rating)
        {
            var instance = new Feedback { FeedbackId = Guid.NewGuid() };
            instance.Update(description, patient, doctor, rating);
            return instance;
        }

        public void Update(string description, Patient patient, Doctor doctor, string rating)
        {
            Description = description;
            CurrentPacient = patient;
            CurrentDoctor = doctor;
            Rating = rating;
        }
    }
}
