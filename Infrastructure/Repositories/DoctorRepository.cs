using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.IRepositories;
using Infrastructure.Context;
using Infrastructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DoctorRepository : EditableBaseRepository<Doctor>
    {
        public DoctorRepository(DatabaseService databaseService) : base(databaseService)
        {
        }
    }
}
