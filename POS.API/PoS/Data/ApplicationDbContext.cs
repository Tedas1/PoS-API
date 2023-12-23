using Microsoft.EntityFrameworkCore;
using PoS.Abstractions;
using PoS.Entities;

namespace PoS.Data
{
    public class ApplicationDbContext: DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public virtual DbSet<User>? Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
