using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class AutoModelConfiguration : IEntityTypeConfiguration<AutoModel>
{
    public void Configure(EntityTypeBuilder<AutoModel> builder)
    {
        builder.HasKey(x => x.Id);



        builder.HasOne(p => p.AutoBrand)
               .WithMany()
               .HasForeignKey(p => p.AutoBrandId);
    }
}
