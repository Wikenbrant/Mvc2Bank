using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

                builder.HasKey(e => e.CustomerId);

                builder.Property(e => e.Birthday).HasColumnType("date");

                builder.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(e => e.CountryCode)
                    .IsRequired()
                    .HasMaxLength(2);

                builder.Property(e => e.Emailaddress).HasMaxLength(100);

                builder.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(6);

                builder.Property(e => e.Givenname)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(e => e.NationalId).HasMaxLength(20);

                builder.Property(e => e.Streetaddress)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(100);

                builder.Property(e => e.Telephonecountrycode).HasMaxLength(10);

                builder.Property(e => e.Telephonenumber).HasMaxLength(25);

                builder.Property(e => e.Zipcode)
                    .IsRequired()
                    .HasMaxLength(15);
        }
    }
}