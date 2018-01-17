using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class PatientHistory
    {
        [Key]
        public Guid HistoryId { get; private set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; private set; }
        public Guid PatientId { get; private set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; private set; }
        public Guid DoctorId { get; private set; }
        public string Prescription { get; private set; }
        public string Description { get; private set; }
        public string Recomandations { get; private set; }
        public DateTime Date { get; private set; }

        private PatientHistory() { }

        public static PatientHistory Create(string prescription, string description, string recomandations, DateTime date)
        {
            var instance = new PatientHistory { HistoryId = Guid.NewGuid() };
            instance.Update(prescription, description, recomandations, date);
            return instance;
        }

        public void Update(string prescription, string description, string recomandations, DateTime date)
        {
            Prescription = prescription;
            Description = description;
            Recomandations = recomandations;
            Date = date;
        }
    }
}
