using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Infrastructure.Data.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(p => p.Permissions)
               .WithMany(p => p.Roles)
               .UsingEntity("RolesPermissions");

        builder.HasMany(p => p.Users)
               .WithOne(p => p.Role)
               .HasForeignKey(p=>p.RoleId);


        List<Role> roles = new List<Role>()
        {
            new Role(){ Id=1,IsActive=true,RoleName=Role.SuperAdmin,Description="کاربر ارشد سیستم"},
            new Role(){ Id=2,IsActive=true,RoleName=Role.Admin,Description="کاربر ارشد"},
            new Role(){ Id=3,IsActive=true,RoleName=Role.ProvinceAdmin,Description="کاربر ناحیه"},
            new Role(){ Id=4,IsActive=true,RoleName=Role.MineUser,Description="کاربر معدن"},
        };

        builder.HasData(roles);
    }
}
