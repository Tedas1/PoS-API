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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User);

            builder.Entity<Order>().HasKey(x => x.Id);
            builder.Entity<Item>().HasKey(x => x.Id);

            builder.Entity<Order>()
                .HasMany(o => o.Items)
                .WithMany(i => i.Orders);
        }
    }
}
