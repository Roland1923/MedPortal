using Core.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.BaseRepositories;

namespace Infrastructure.Repositories
{
    public class PatientRepository : EditableBaseRepository<Patient>
    {
        public PatientRepository(DatabaseService databaseService) : base(databaseService)
        {
        }
    }
}
