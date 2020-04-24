using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(e => e.TransactionId)
                .HasName("PK_trans2");

            builder.Property(e => e.Account).HasMaxLength(50);

            builder.Property(e => e.Amount).HasColumnType("decimal(13, 2)");

            builder.Property(e => e.Balance).HasColumnType("decimal(13, 2)");

            builder.Property(e => e.Bank).HasMaxLength(50);

            builder.Property(e => e.Date).HasColumnType("date");

            builder.Property(e => e.Operation)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Symbol).HasMaxLength(50);

            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.AccountNavigation)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Transactions_Accounts");
        }
    }
}