using Driver.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moneyon.Common.Extensions;

namespace Infrastructure.Data.Configuration;

public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.HasKey(x => x.Id);

        List<Permission> permissions = new List<Permission>();
        int id = 1;
        foreach (var permission in Enum.GetValues(typeof(PermissionEnum)))
        {
            permissions.Add(new Permission
            {
                Id= id++,
                Description = ((PermissionEnum)permission).GetDescription(),
                Name = permission.ToString(),
                PermisionId = (int)permission
            });
        }

        builder.HasData(permissions);

    }
}