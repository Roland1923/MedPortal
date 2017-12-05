using System;
using System.Collections.Generic;
using Core.Entities;

namespace Core.IRepositories
{
    public interface IFeedbackRepository
    {
        void AddFeedback(Feedback feedback);
        void EditFeedback(Feedback feedback);
        void DeleteFeedback(Guid id);
        IReadOnlyCollection<Feedback> GetAllFeedbacks();
        Feedback GetFeedbackById(Guid id);
    }
}
