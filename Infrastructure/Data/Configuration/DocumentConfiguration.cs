using Driver.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(p => p.Content)
               .WithOne(p => p.Document)
               .HasPrincipalKey<Document>(x => x.Id)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
