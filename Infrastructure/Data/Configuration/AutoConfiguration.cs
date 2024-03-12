using Domain.Entities;
using Driver.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infrastructure.Data.Configuration;

public class AutoConfiguration : IEntityTypeConfiguration<Auto>
{
    public void Configure(EntityTypeBuilder<Auto> builder)
    {
        builder.HasKey(x => x.Id);
        //builder.Property(p => p.Pelak)
        //        .HasComputedColumnSql("[PelakLeft]+' '+ [PelakCharekter]+' '+ [PelakRight]+' - '+[PelakCountryNum]+' '+'ایران'");

        //builder.HasOne(p => p.AutoModel)
        //       .WithOne()
        //       .HasForeignKey<Auto>(p=>p.AutoModelId);

        //builder.HasOne(p => p.Image)
        //       .WithOne()
        //       .HasForeignKey<Document>(p => p.AutoId)
        //       .OnDelete(DeleteBehavior.NoAction); 

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
