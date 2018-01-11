using System;
using Core.Entities;

namespace Web.Models
{
    public class AppointmentModel
    {
        public DateTime AppointmentDate { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
    }
}
