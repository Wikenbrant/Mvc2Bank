using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(e => e.UserId).HasColumnName("UserID");

            builder.Property(e => e.FirstName).HasMaxLength(40);

            builder.Property(e => e.LastName).HasMaxLength(40);

            builder.Property(e => e.LoginName)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(e => e.PasswordHash)
                .IsRequired()
                .HasMaxLength(64);
        }
    }
}