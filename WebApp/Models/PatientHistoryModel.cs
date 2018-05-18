using System;

namespace WebApp.Models
{
    public class PatientHistoryModel
    {
        public Guid PatientId { get; set; }
        public Guid DoctorId { get; set; }
        public string Prescription { get; set; }
        public string Description { get; set; }
        public string Recommendations { get; set; }
        public DateTime Date { get; set; }
    }
}
