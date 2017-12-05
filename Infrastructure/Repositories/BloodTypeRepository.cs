using System;
using System.Collections.Generic;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Context;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class BloodTypeRepository : IBloodTypeRepository
    {
        private readonly IDatabaseService _databaseService;

        public BloodTypeRepository(IDatabaseService BloodTypeService)
        {
            _databaseService = BloodTypeService;
        }

        public void AddBloodType(BloodType BloodType)
        {
            _databaseService.BloodTypes.Add(BloodType);
            _databaseService.SaveChanges();
        }

        public void EditBloodType(BloodType BloodType)
        {
            _databaseService.BloodTypes.Update(BloodType);
            _databaseService.SaveChanges();
        }

        public void DeleteBloodType(Guid id)
        {
            var BloodType = GetBloodTypeById(id);
            _databaseService.BloodTypes.Remove(BloodType);
            _databaseService.SaveChanges();
        }

        public IReadOnlyCollection<BloodType> GetAllBloodTypes()
        {
            return _databaseService.BloodTypes.ToList();
        }

        public BloodType GetBloodTypeById(Guid id)
        {
            return _databaseService.BloodTypes.FirstOrDefault(t => t.BloodTypeId == id);
        }
    }
}
