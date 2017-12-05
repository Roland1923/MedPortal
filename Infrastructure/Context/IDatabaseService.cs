using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Context
{
    public interface IDatabaseService
    {
        DbSet<Patient> Patients { get; set; }
        DbSet<Doctor> Doctors { get; set; }
        DbSet<PatientHistory> PatientHistories { get; set; }
        DbSet<Appointment> Appointments { get; set; }
        DbSet<Feedback> Feedbacks { get; set; }
        DbSet<BloodDonor> BloodDonors { get; set; }
        EntityEntry Entry(object entity);
        int SaveChanges();
    }
}

