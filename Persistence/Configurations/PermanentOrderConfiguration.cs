using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class PermanentOrderConfiguration : IEntityTypeConfiguration<PermanentOrder>
    {
        public void Configure(EntityTypeBuilder<PermanentOrder> builder)
        {
            builder.ToTable("PermenentOrder");
            builder.HasKey(e => e.OrderId);

            builder.Property(e => e.AccountTo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Amount).HasColumnType("decimal(13, 2)");

            builder.Property(e => e.BankTo)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Symbol)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Account)
                .WithMany(p => p.PermenentOrder)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_PermenentOrder_Accounts");
        }
    }
}