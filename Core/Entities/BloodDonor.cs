using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities
{
    public class BloodDonor
    {
        [Required]
        public Guid BloodDonorID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string BloodGroup { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsVerified { get; set; }
    }
}
