using Microsoft.EntityFrameworkCore;
using PoS.Entities;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PoS.Abstractions
{
    public interface IApplicationDbContext: IDisposable
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
