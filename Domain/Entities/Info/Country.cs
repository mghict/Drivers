using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Countries")]
public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Province>? Provinces { get; set; }
}
