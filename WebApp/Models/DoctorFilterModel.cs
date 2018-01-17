using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class DoctorFilterModel
    {
        public string Name { get; set; }
        public string Hospital { get; set; }
        public string Speciality { get; set; }
        public string City { get; set; }
    }
}
