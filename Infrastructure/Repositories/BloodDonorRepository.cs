using Core.Entities;
using Infrastructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BloodDonorRepository : EditableBaseRepository<BloodDonor>
    {
        public BloodDonorRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
