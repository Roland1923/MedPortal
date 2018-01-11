using Core.Entities;

namespace WebApp.Models
{
    public class BloodBankModel
    {
        public string Type { get; set; }
        public Doctor Doctor { get; set; }
    }
}
