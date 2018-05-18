using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class BloodDonor
    {
        [Key]
        public Guid BloodDonorId { get; private set; }
        public string Type { get; private set; }
        [ForeignKey("PatientId")]
        public Patient Patient => null;
        public Guid PatientId { get; private set; }

        private BloodDonor() { }

        public static BloodDonor Create(string type, Guid patientId)
        {
            var instance = new BloodDonor { BloodDonorId = Guid.NewGuid() };
            instance.Update(type, patientId);
            return instance;
        }

        public void Update(string type, Guid patientId)
        {
            Type = type;
            PatientId = patientId;
        }
    }
}
