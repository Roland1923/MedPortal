using Core.Entities;
using Infrastructure.Context;
using Infrastructure.Repositories.BaseRepositories;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository : EditableBaseRepository<Appointment>
    {
        public AppointmentRepository(DatabaseService databaseService) : base(databaseService)
        {
        }
    }
}
