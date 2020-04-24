using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasKey(e => e.LoanId)
                .HasName("PK_loan");

            builder.Property(e => e.Amount).HasColumnType("decimal(13, 2)");

            builder.Property(e => e.Date).HasColumnType("date");

            builder.Property(e => e.Payments).HasColumnType("decimal(13, 2)");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Account)
                .WithMany(p => p.Loans)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Loans_Accounts");
        }
    }
}