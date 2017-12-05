using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IDatabaseService _databaseService;

        public FeedbackRepository(IDatabaseService doctorService)
        {
            _databaseService = doctorService;
        }

        public void AddFeedback(Feedback feedback)
        {
            _databaseService.Feedbacks.Add(feedback);
            _databaseService.SaveChanges();
        }

        public void EditFeedback(Feedback feedback)
        {
            _databaseService.Feedbacks.Update(feedback);
            _databaseService.SaveChanges();
        }

        public void DeleteFeedback(Guid id)
        {
            var doctor = GetFeedbackById(id);
            _databaseService.Feedbacks.Remove(doctor);
            _databaseService.SaveChanges();
        }

        public IReadOnlyCollection<Feedback> GetAllFeedbackss()
        {
            return _databaseService.Feedbacks.ToList();
        }

        public Feedback GetFeedbackById(Guid id)
        {
            return _databaseService.Feedbacks.FirstOrDefault(t => t.FeedbackId == id);
        }

        public IReadOnlyCollection<Feedback> GetAllFeedbacks()
        {
            throw new NotImplementedException();
        }
    }
}
