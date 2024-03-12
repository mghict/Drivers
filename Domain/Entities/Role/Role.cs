using Domain.Interface;
using Driver.Domain.Entities;
using Moneyon.Common.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Roles")]
public class Role:Entity<int>, IAudit
{
    public const string SuperAdmin = "SUPER_ADMIN"; 
    public const string Admin = "ADMIN"; 
    public const string ProvinceAdmin = "PROVINCE_ADMIN"; 
    public const string MineUser = "MINE_USER"; 

    [MaxLength(150)]
    public string RoleName { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }
    public bool IsActive { get; set; }

    [Column(TypeName ="datetime")]
    public DateTime CreatedOn { get ; set ; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get ; set ; }

    public virtual ICollection<Permission>? Permissions { get; set; }
    public virtual ICollection<User>? Users { get; set; }

}
