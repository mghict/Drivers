using Driver.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class RecievedWeightConfiguration : IEntityTypeConfiguration<RecievedWeight>
{
    public void Configure(EntityTypeBuilder<RecievedWeight> builder)
    {
        builder.HasKey(x => x.Id);
        //builder.HasOne(p => p.RecievedMission)
        //       .WithOne()
        //       .HasForeignKey<RecievedMission>(p => p.RecievedWeightId);

        //builder.HasMany(p => p.RecievedMissions)
        //       .WithOne()
        //       .HasForeignKey(p => p.AutoId);

        //builder.HasMany(p => p.RecievedSpeedAndTempratures)
        //       .WithOne()
        //       .HasForeignKey(p => p.AutoId);

        //builder.HasMany(p => p.RecievedWeights)
        //       .WithOne()
        //       .HasForeignKey(p => p.AutoId);

        //builder.HasMany(p => p.RecievedErrors)
        //       .WithOne()
        //       .HasForeignKey(p => p.AutoId);
    }
}