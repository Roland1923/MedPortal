using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.IRepositories
{
    public interface IBloodDonorRepository
    {
        void AddBloodDonor(BloodDonor b);
        void EditBloodDonor(BloodDonor b);
        void DeleteBloodDonor(Guid id);
        IReadOnlyCollection<BloodDonor> GetAllBloodDonors();
        BloodDonor GetBloodDonorById(Guid id);
    }

}
