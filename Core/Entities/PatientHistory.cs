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
        public Patient Patient => null;
        public Guid PatientId { get; private set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor => null;
        public Guid DoctorId { get; private set; }
        public string Prescription { get; private set; }
        public string Description { get; private set; }
        public string Recommendation { get; private set; }
        public DateTime Date { get; private set; }

        private PatientHistory() { }

        public static PatientHistory Create(Guid patientId, Guid doctorId, string prescription, string description, string recommendation, DateTime date)
        {
            var instance = new PatientHistory { HistoryId = Guid.NewGuid() };
            instance.Update(patientId, doctorId, prescription, description, recommendation, date);
            return instance;
        }

        public void Update(Guid patientId, Guid doctorId, string prescription, string description, string recommendation, DateTime date)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            Prescription = prescription;
            Description = description;
            Recommendation = recommendation;
            Date = date;
        }
    }
}
