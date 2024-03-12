using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Interface;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Persons")]
[Index(nameof(PersonCode), Name = "IX_Person_PersonCode")]
public class Person:Entity<long>, IAudit
{
    //[Key]
    //[Column(Order =0)]
    //public long Id { get; set; }

    //[Column(Order =1)]
    public Guid PersonCode { get; set; }

    [MaxLength(150)]
    public string FirstName { get; set; }

    [MaxLength(150)]
    public string LastName { get; set; }

    [MaxLength(15)]
    public string NationalCode { get; set; }

    [MaxLength(150)]
    public string? Email { get; set; }
    
    [Column(TypeName ="varchar(15)")]
    public string MobileNumber { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [MaxLength(300)]
    public string DisplayName
    {
        get => (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName)) ? MobileNumber : $"{FirstName} {LastName}";
        private set { }
    }

    [Column(TypeName = "datetime")]
    public DateTime BirthDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedOn { get ; set ; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set ; }

    public bool IsActive { get; set; }
    public virtual User User { get; set; }

    public long? DocumentId { get; set; }
    [ForeignKey("DocumentId")]
    public Driver.Domain.Entities.Document? Document { get; set; }
    public virtual ICollection<Auto>? Autos { get; set; }

    public PersonType Type { get; set; } = PersonType.Users;
    public Person()
    {
        PersonCode=Guid.NewGuid();
    }

}


