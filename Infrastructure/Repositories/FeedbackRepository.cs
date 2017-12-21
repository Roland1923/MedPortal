using Core.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.BaseRepositories;

namespace Infrastructure.Repositories
{
    public class FeedbackRepository : EditableBaseRepository<Feedback>
    {
        public FeedbackRepository(DatabaseService databaseService) : base(databaseService)
        {
        }
    }
}
