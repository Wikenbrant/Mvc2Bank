using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class DispositionConfiguration : IEntityTypeConfiguration<Disposition>
    {
        public void Configure(EntityTypeBuilder<Disposition> builder)
        {
            builder.ToTable("Dispositions");

            builder.HasKey(e => e.DispositionId)
                .HasName("PK_disposition");

            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Account)
                .WithMany(p => p.Dispositions)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Dispositions_Accounts");

            builder.HasOne(d => d.Customer)
                .WithMany(p => p.Dispositions)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Dispositions_Customers");
        }
    }
}