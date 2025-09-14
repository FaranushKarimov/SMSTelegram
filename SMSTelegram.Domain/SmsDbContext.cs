using Microsoft.EntityFrameworkCore;
using SMSTelegram.Domain.Entities;

namespace SMSTelegram.Domain;

public sealed class SmsDbContext:DbContext
{
    public SmsDbContext(DbContextOptions<SmsDbContext> options) : base(options)
    {
        // if (Database.GetPendingMigrations().Any())
        // {
        //     Database.Migrate();
        // }
    }   
    public DbSet<UserEntity> Users { get; set; } = null!;
}