using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Context
{
    public interface IDatabaseService
    {
        DbSet<Patient> Patients { get; set; }
        EntityEntry Entry(object entity);
        int SaveChanges();
    }
}

