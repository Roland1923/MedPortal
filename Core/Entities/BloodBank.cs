using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class BloodBank
    {
        [Key]
        public Guid BloodBankId { get; private set; }
        [ForeignKey("Type")]
        public Dictionary<string,int> Types { get; private set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; private set; }
        public Guid DoctorId { get; private set; }

        private BloodBank() { }

        public static BloodBank Create(Doctor doctor)
        {
            var instance = new BloodBank { BloodBankId = Guid.NewGuid() };
            instance.Update("", doctor);
            instance.Types = new Dictionary<string, int>();
            return instance;
        }

        public void Update(string type,  Doctor doctor)
        {
            Types[type]++;
            Doctor = doctor;
        }
    }
}
