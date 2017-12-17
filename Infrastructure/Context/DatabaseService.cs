using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Infrastructure.Context
{
    public sealed class DatabaseService : DbContext, IDatabaseService
    {
        public DatabaseService(DbContextOptions<DatabaseService> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Core.Entities.Patient> Patients { get; set; }
        public DbSet<Core.Entities.Doctor> Doctors { get; set; }
        public DbSet<Core.Entities.PatientHistory> PatientHistories { get; set; }
        public DbSet<Core.Entities.Appointment> Appointments { get; set; }
        public DbSet<Core.Entities.Feedback> Feedbacks { get; set; }
        public DbSet<Core.Entities.BloodDonor> BloodDonors { get; set; }
        
    }
}