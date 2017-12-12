using Core.Entities;
using Infrastructure.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository : EditableBaseRepository<Appointment>
    {
        public AppointmentRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
