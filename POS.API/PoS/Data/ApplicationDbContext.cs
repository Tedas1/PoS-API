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
        public virtual DbSet<Item>? Item { get; set; }
        public virtual DbSet<Order>? Order { get; set; }
        public virtual DbSet<ItemOrder>? ItemOrder { get; set; }

        public virtual DbSet<Tip>? Tip { get; set; }

        public virtual DbSet<Tax>? Tax { get; set; }
        public virtual DbSet<Payment>? Payment { get; set; }

        public virtual DbSet<Reservation>? Reservation { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasKey(x => x.Id);
            builder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User);

            builder.Entity<Order>().HasKey(x => x.Id);
            builder.Entity<Item>().HasKey(x => x.Id);

            builder.Entity<ItemOrder>()
                .HasKey(io => new { io.OrderId, io.ItemId });

            builder.Entity<ItemOrder>()
                .HasOne<Item>()
                .WithMany()
                .HasForeignKey(io => io.ItemId);

            builder.Entity<ItemOrder>()
                .HasOne<Order>()
                .WithMany()
                .HasForeignKey(io => io.OrderId);

            builder.Entity<Tip>()
                .HasOne<Order>()
                .WithOne()
                .HasForeignKey<Tip>(t => t.OrderId);

            builder.Entity<Tax>()
                .HasKey(t => t.TaxId);

            builder.Entity<Payment>()
                .HasOne<Order>()
                .WithOne()
                .HasForeignKey<Payment>(t => t.OrderId);

            builder.Entity<Reservation>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.CustomerId);

            builder.Entity<Reservation>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Reservation>()
                .HasOne<Order>()
                .WithMany()
                .HasForeignKey(r => r.OrderId);
        }
    }
}
