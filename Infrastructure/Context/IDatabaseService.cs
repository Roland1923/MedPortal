using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public interface IDatabaseService
    {
        DbSet<BloodDonor> BloodDonors { get; set; }
        int SaveChanges();
    }
}
