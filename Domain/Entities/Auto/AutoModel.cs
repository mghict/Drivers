using Moneyon.Common.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("AutoModels")]
public class AutoModel:Entity<int>
{
    //public int Id { get; set; }

    [MaxLength(150)]
    public string Name { get; set; }
    public int AutoBrandId { get; set; }
    public AutoBrand AutoBrand { get; set; }
    public ICollection<Auto>? Autos { get; set; }
}
