using System;
using System.Collections.Generic;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Context;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IDatabaseService _databaseService;

        public DoctorRepository(IDatabaseService doctorService)
        {
            _databaseService = doctorService;
        }

        public void AddDoctor(Doctor doctor)
        {
            _databaseService.Doctors.Add(doctor);
            _databaseService.SaveChanges();
        }

        public void EditDoctor(Doctor doctor)
        {
            _databaseService.Doctors.Update(doctor);
            _databaseService.SaveChanges();
        }

        public void DeleteDoctor(Guid id)
        {
            var doctor = GetDoctorById(id);
            _databaseService.Doctors.Remove(doctor);
            _databaseService.SaveChanges();
        }

        public IReadOnlyCollection<Doctor> GetAllDoctors()
        {
            return _databaseService.Doctors.ToList();
        }

        public Doctor GetDoctorById(Guid id)
        {
            return _databaseService.Doctors.FirstOrDefault(t => t.DoctorId == id);
        }
    }
}
