using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.IRepositories
{
    public interface IPatientHistoryRepository
    {
        void AddPatientHistory(PatientHistory patientHistory);
        void EditPatientHistory(PatientHistory patientHistory);
        void DeletePatientHistory(Guid id);
        IReadOnlyCollection<PatientHistory> GetAllPatientHistories();
        PatientHistory GetPatientHistoryById(Guid id); // by ID Pacient
    }
}
