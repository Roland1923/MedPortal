using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.IRepositories
{
    public interface IBloodTypeRepository
    {
        void AddBloodType(BloodType BloodType);
        void EditBloodType(BloodType BloodType);
        void DeleteBloodType(Guid id);
        IReadOnlyCollection<BloodType> GetAllBloodTypes();
        BloodType GetBloodTypeById(Guid id);
    }
}
