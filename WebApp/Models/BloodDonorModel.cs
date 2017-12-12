using Core.Entities;

namespace WebApp.Models
{
    public class BloodDonorModel
    {
        public string Type { get; set; }
        public Patient Patient { get; set; }
    }
}
