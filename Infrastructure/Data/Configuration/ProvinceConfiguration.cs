using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(p => p.Country)
            .WithMany(p => p.Provinces)
            .HasForeignKey(p => p.CountryId)
            .IsRequired();

        builder.HasMany(p => p.Cities)
            .WithOne(p => p.Province)
            .HasForeignKey(p => p.ProvinceId)
            .IsRequired();
    }
}
