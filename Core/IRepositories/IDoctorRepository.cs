using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.IRepositories
{
    public interface IDoctorRepository
    {
        void AddDoctor(Doctor doctor);
        void EditDoctor(Doctor doctor);
        void DeleteDoctor(Guid id);
        IReadOnlyCollection<Doctor> GetAllDoctors();
        Doctor GetDoctorById(Guid id);
    }
}
