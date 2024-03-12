using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class MineConfiguration : IEntityTypeConfiguration<Mine>
{
    public void Configure(EntityTypeBuilder<Mine> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(p => p.Location)
            .WithOne()
            .HasForeignKey<Mine>(p => p.LocationId);

        builder.HasMany(p => p.Autos)
            .WithOne(p => p.Mine)
            .HasForeignKey(p => p.MineId);
    }
}
