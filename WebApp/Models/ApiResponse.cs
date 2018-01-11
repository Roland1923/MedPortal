using Core.Entities;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Models
{
    public class ApiResponse
    {
        public bool Status { get; set; }
        public Appointment Appointment { get; set; }
        public BloodDonor BloodDonor { get; set; }
        public Feedback Feedback { get; set; }
        public BloodBank BloodBank { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public PatientHistory PatientHistory { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}
