using System;
using Core.Entities;

namespace WebApp.Models
{
    public class AppointmentModel
    {
        public DateTime AppointmentDate { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
