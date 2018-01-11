using Core.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.BaseRepositories;

namespace Infrastructure.Repositories
{
    public class BloodBankRepository : EditableBaseRepository<BloodBank>
    {
        public BloodBankRepository(DatabaseService databaseService) : base(databaseService)
        {
        }
    }
}
