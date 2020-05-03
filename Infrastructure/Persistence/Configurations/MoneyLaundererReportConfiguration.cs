using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class MoneyLaundererReportConfiguration : IEntityTypeConfiguration<MoneyLaundererReport>
    {
        public void Configure(EntityTypeBuilder<MoneyLaundererReport> builder)
        {
            builder.ToTable("MoneyLaunderers");

            builder.HasKey(e => e.MoneyLaundererId);


            builder.Property(e => e.StartDate).HasColumnType("date");

            builder.Property(e => e.EndDate).HasColumnType("date");

            builder.Property(e => e.Succeeded).HasDefaultValue(false);
        }
    }
}