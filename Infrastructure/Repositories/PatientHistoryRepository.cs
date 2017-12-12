using Core.Entities;
using Infrastructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PatientHistoryRepository : EditableBaseRepository<PatientHistory>
    {
        public PatientHistoryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
