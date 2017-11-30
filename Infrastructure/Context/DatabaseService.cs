using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class DatabaseService : DbContext, IDatabaseService
    {
        public DatabaseService(DbContextOptions<DatabaseService> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<BloodDonor> BloodDonors { get; set; }
    }
}
