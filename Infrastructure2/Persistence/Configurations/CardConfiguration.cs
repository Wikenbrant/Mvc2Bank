using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.HasKey(e => e.CardId);

            builder.Property(e => e.Ccnumber)
                .IsRequired()
                .HasColumnName("CCNumber")
                .HasMaxLength(50);

            builder.Property(e => e.Cctype)
                .IsRequired()
                .HasColumnName("CCType")
                .HasMaxLength(50);

            builder.Property(e => e.Cvv2)
                .IsRequired()
                .HasColumnName("CVV2")
                .HasMaxLength(10);

            builder.Property(e => e.Issued).HasColumnType("date");

            builder.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasOne(d => d.Disposition)
                .WithMany(p => p.Cards)
                .HasForeignKey(d => d.DispositionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cards_Dispositions");
        }
    }
}