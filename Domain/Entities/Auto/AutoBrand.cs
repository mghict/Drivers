using Moneyon.Common.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("AutoBrands")]
public class AutoBrand:Entity<int>
{

    [MaxLength(150)]
    public string Name { get; set; }

    //public virtual ICollection<AutoModel>? Models { get; set; }
}
