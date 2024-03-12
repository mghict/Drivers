using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security;

namespace Infrastructure.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(p => p.Mines)
               .WithMany(p => p.Users)
               .UsingEntity("UsersMines");

        builder.HasMany(p => p.Provinces)
               .WithMany(p => p.Users)
               .UsingEntity("UsersProvinces");

    }
}
