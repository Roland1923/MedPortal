using System;
using System.Collections.Generic;
using System.Linq;
using Core.IRepositories;
using Core.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class PatientHistoryRepository : IPatientHistoryRepository
    {
        private readonly IDatabaseService _databaseService;

        public PatientHistoryRepository(IDatabaseService patientHistoryService)
        {
            _databaseService = patientHistoryService;
        }

        public void AddPatientHistory(PatientHistory patientHistory)
        {
            _databaseService.PatientHistories.Add(patientHistory);
            _databaseService.SaveChanges();
        }

        public void EditPatientHistory(PatientHistory patientHistory)
        {
            _databaseService.PatientHistories.Update(patientHistory);
            _databaseService.SaveChanges();
        }

        public void DeletePatientHistory(Guid id)
        {
            var patientHistory = GetPatientHistoryById(id);
            _databaseService.PatientHistories.Remove(patientHistory);
            _databaseService.SaveChanges();
        }

        public PatientHistory GetPatientHistoryById(Guid id)
        {
            return _databaseService.PatientHistories.FirstOrDefault(t => t.HistoryId == id);
        }

        public IReadOnlyCollection<PatientHistory> GetAllPatientHistories()
        {
            return _databaseService.PatientHistories.ToList();
        }
    }
}
