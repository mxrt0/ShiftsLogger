using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Data.Entities;

namespace ShiftsLogger.Data.Context;

public class ShiftsDbContext : DbContext
{
    public ShiftsDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Worker> Workers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Worker>()
            .HasIndex(w => w.Name)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
