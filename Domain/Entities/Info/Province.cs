using Moneyon.Common.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("Provinces")]
public class Province:Entity<int>
{
    //public int Id { get; set; }
    public string Name { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public virtual ICollection<City>? Cities { get; set; }
    public virtual ICollection<User>? Users { get; set; }
}
