using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.IRepositories
{
    public interface IPatientRepository
    {
        void AddPatient(Patient patient);
        void EditPatient(Patient patient);
        void DeletePatient(Guid id);
        IReadOnlyCollection<Patient> GetAllPatients();
        Patient GetPatientById(Guid id);
    }

}
