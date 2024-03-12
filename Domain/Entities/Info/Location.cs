using Common.Models;
using Moneyon.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;


[Table("Locations")]
public class Location:Entity<long>
{
    //public long Id { get; set; }
    public int CityId { get; set; }
    public City City { get; set; }

    [MaxLength(150)]
    public string Name { get; set; }
    
    [Column(TypeName ="decimal(12,8)")]    
    public decimal lat { get; set; }

    [Column(TypeName = "decimal(12,8)")]
    public decimal lng { get; set; }
}
