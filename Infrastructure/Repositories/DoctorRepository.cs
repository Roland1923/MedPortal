using Core.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.BaseRepositories;

namespace Infrastructure.Repositories
{
    public class DoctorRepository : EditableBaseRepository<Doctor>
    {
        public DoctorRepository(DatabaseService databaseService) : base(databaseService)
        {
        }
    }
}
