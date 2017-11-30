using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class BloodDonorRepository : IBloodDonorRepository
    {
        private readonly IDatabaseService _databaseService;

        public BloodDonorRepository(IDatabaseService bloodDonorService)
        {
            _databaseService = bloodDonorService;
        }

        public void AddBloodDonor(BloodDonor bloodDonor)
        {
            _databaseService.BloodDonors.Add(bloodDonor);
            _databaseService.SaveChanges();
        }

        public void EditBloodDonor(BloodDonor bloodDonor)
        {
            _databaseService.BloodDonors.Update(bloodDonor);
            _databaseService.SaveChanges();
        }

        public void DeleteBloodDonor(Guid id)
        {
            var bloodDonor = GetBloodDonorById(id);
            _databaseService.BloodDonors.Remove(bloodDonor);
            _databaseService.SaveChanges();
        }

        public BloodDonor GetBloodDonorById(Guid id)
        {
            return _databaseService.BloodDonors.FirstOrDefault(t => t.BloodDonorID == id);
        }

        public IReadOnlyCollection<BloodDonor> GetAllBloodDonors()
        {
            return _databaseService.BloodDonors.ToList();
        }
    }
}
