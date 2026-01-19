using Microsoft.EntityFrameworkCore;
using SMSTelegram.Domain.Entities;

namespace SMSTelegram.Domain;

public sealed class SmsDbContext : DbContext
{
    public SmsDbContext(DbContextOptions<SmsDbContext> options) : base(options)
    {
        // if (Database.GetPendingMigrations().Any())
        // {
        //     Database.Migrate();
        // }
    }

    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>()
            .HasIndex(c => c.Code);
        
        base.OnModelCreating(modelBuilder);
    }
}