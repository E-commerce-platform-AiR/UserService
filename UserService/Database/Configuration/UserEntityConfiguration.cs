using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Database.Entities;

namespace UserService.Database.Configuration;

public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("user_entity");

        builder.HasKey(x => x.Id)
            .HasName("PK_user_entity");

        builder.Property(x => x.Id)
            .HasColumnName("user_id");

        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("user_name");

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("password");

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(255)
            .HasColumnName("email");
    }
}