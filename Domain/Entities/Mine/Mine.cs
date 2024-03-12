using Driver.Domain.Entities;
using Moneyon.Common.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("MineDefinations")]
public class Mine:Entity<long>
{
    public long MineCode { get; set; } = 0;

    [MaxLength(150)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Address { get; set; }
    
    public long LocationId { get; set; }
    public Location Location { get; set; }

    public virtual ICollection<Auto>? Autos { get; set; }
    public virtual ICollection<User>? Users { get; set; }
    public virtual ICollection<Material>? Materials { get; set; }
}
