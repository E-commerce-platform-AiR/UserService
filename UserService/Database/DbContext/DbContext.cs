using Microsoft.EntityFrameworkCore;
using UserService.Database.Configuration;
using UserService.Database.Entities;

namespace UserService.Database.DbContext;

public class UserDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
    
    public DbSet<UserEntity> Users { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
    }
    public void Seed()
    {
        if (!Users.Any(u => u.Email == "admin@example.com"))
        {
            Users.Add(new UserEntity
            {
                Id = Guid.NewGuid(),
                UserName = "admin",
                Password = "admin", 
                Email = "admin@example.com",
                IsAdmin = true
            });

            SaveChanges();
        }
    }
}