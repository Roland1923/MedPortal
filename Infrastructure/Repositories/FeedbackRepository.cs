using Core.Entities;
using Infrastructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FeedbackRepository : EditableBaseRepository<Feedback>
    {
        public FeedbackRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
