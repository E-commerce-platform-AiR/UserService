using Microsoft.EntityFrameworkCore;
using UserService.Database.Configuration;
using UserService.Database.Entities;

namespace UserService.Database.DbContext;

public class ShopDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }
    
    public DbSet<UserEntity> Users { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
    }
}