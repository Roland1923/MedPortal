using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Context
{
    public interface IDatabaseService
    {
        EntityEntry Entry(object entity);
        int SaveChanges();
    }
}

