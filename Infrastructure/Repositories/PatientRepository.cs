using Core.Entities;
using Infrastructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PatientRepository : EditableBaseRepository<Patient>
    {
        public PatientRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
