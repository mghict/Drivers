using Moneyon.Common.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Cites")]
public class City:Entity<int>
{
    //public int Id { get; set; }
    public string Name { get; set; }
    public int ProvinceId { get; set; }
    public Province Province { get; set; }

    public virtual ICollection<Location> Locations { get; set; }
}
