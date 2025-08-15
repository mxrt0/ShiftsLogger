using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Data.Entities;

namespace ShiftsLogger.Data.Context;

public class ShiftsDbContext : DbContext
{
    public ShiftsDbContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Shift> Shifts { get; set; }
}
