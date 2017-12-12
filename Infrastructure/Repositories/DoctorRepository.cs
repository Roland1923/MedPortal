using Core.Entities;
using Infrastructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DoctorRepository : EditableBaseRepository<Doctor>
    {
        public DoctorRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
