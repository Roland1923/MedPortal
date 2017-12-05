using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class BloodType
    {
        private BloodType() { }

        public static BloodType Create(string type)
        {
            var instance = new BloodType { BloodTypeId = Guid.NewGuid(), Patients = new List<Patient>() };
            instance.Update(type);
            return instance;
        }

        public void Update(string type)
        {
            Type = type;
        }

        [Key]
        [Required]
        public Guid BloodTypeId { get; private set; }

        [Required(ErrorMessage = "You must provide a valid Blood Type!")]
        [Display(Name = "Blood Type (O1, A2, B3, AB4)")]
        [RegularExpression(@"^(01|A2|B3|AB4)$", ErrorMessage = "Not a valid Blood Type!")]
        public string Type { get; private set; }

        //[Required]
        //Putem instantia initial empty?
        public List<Patient> Patients { get; private set; }
    }
}
