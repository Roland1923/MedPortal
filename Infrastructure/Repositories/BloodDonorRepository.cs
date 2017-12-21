using Core.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.BaseRepositories;

namespace Infrastructure.Repositories
{
    public class BloodDonorRepository : EditableBaseRepository<BloodDonor>
    {
        public BloodDonorRepository(DatabaseService databaseService) : base(databaseService)
        {
        }
    }
}
