using Core.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.BaseRepositories;

namespace Infrastructure.Repositories
{
    public class PatientHistoryRepository : EditableBaseRepository<PatientHistory>
    {
        public PatientHistoryRepository(DatabaseService databaseService) : base(databaseService)
        {
        }
    }
}
