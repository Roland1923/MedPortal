﻿using Microsoft.EntityFrameworkCore;

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
        public DbSet<PatientHistory> PatientHistories { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<BloodDonor> BloodDonors { get; set; }
    }
}