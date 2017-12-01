using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public sealed class DatabaseService : DbContext, IDatabaseService
    {
        public DatabaseService(DbContextOptions<DatabaseService> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Core.Entities.Patient> Patients { get; set; }
    }
}