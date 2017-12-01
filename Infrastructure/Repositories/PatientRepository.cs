using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly IDatabaseService _databaseService;

        public PatientRepository(IDatabaseService patientService)
        {
            _databaseService = patientService;
        }

        public void AddPatient(Patient patient)
        {
            _databaseService.Patients.Add(patient);
            _databaseService.SaveChanges();
        }

        public void EditPatient(Patient patient)
        {
            _databaseService.Patients.Update(patient);
            _databaseService.SaveChanges();
        }

        public void DeletePatient(Guid id)
        {
            var patient = GetPatientById(id);
            _databaseService.Patients.Remove(patient);
            _databaseService.SaveChanges();
        }

        public Patient GetPatientById(Guid id)
        {
            return _databaseService.Patients.FirstOrDefault(t => t.PatientId == id);
        }

        public IReadOnlyCollection<Patient> GetAllPatients()
        {
            return _databaseService.Patients.ToList();
        }
    }
}
