using Domain.Entities;
using Driver.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configuration;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(p => p.User)
            .WithOne(p => p.Person)
            .HasForeignKey<User>(p => p.PersonId)
            .IsRequired();
        
        //builder.HasOne(p => p.Image)
        //       .WithOne()
        //       .HasForeignKey<Document>(p => p.PersonId)
        //       .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(p => p.Autos)
            .WithOne(p => p.Person)
            .HasForeignKey(p => p.PersonId);

        builder.Property(p => p.DisplayName)
                .HasComputedColumnSql("Case  when [FirstName] is null and [LastName] is null then [MobileNumber] else [FirstName]+' '+[LastName] end");
    }
}

