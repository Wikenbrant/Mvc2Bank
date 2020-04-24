using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(e => e.AccountId)
                .HasName("PK_account");

            builder.Property(e => e.Balance).HasColumnType("decimal(13, 2)");

            builder.Property(e => e.Created).HasColumnType("date");

            builder.Property(e => e.Frequency)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}