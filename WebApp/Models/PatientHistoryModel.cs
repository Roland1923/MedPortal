using System;
using Core.Entities;

namespace WebApp.Models
{
    public class PatientHistoryModel
    {
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public string Prescription { get; set; }
        public string Description { get; set; }
        public string Recomandations { get; set; }
        public DateTime Date { get; set; }
    }
}
