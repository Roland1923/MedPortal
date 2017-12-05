using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class PatientHistory
    {
        private PatientHistory() { }

        [Key]
        [Required]
        public Guid HistoryId { get; private set; }

       [Required]
        public Patient Patient { get; private set; }

        [Required]
        public Doctor Doctor { get; private set; }

        [Display(Name = "Prescription")]
        [MinLength(3), MaxLength(250)]
        public string Prescription { get; private set; }

        [Display(Name = "Description")]
        [MinLength(3), MaxLength(250)]
        public string Description { get; private set; }


        [Display(Name = "Recomandations")]
        [MinLength(3), MaxLength(250)]
        public string Recomandations { get; private set; }


        public static PatientHistory Create(Patient patient, Doctor doctor, string prescription, string description, string recomandations)
        {
            var instance = new PatientHistory { HistoryId = Guid.NewGuid() };
            instance.Update(patient, doctor, prescription, description, recomandations);
            return instance;
        }

        public void Update(Patient patient, Doctor doctor, string prescription, string description, string recomandations)
        {
            Patient = patient;
            Doctor = doctor;
            Prescription = prescription;
            Description = description;
            Recomandations = recomandations;
        }
    }
}
