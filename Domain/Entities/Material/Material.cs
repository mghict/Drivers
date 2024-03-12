using Domain.Entities;
using Driver.Common.Models;
using Moneyon.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Domain.Entities;

[Table("Materials")]
public class Material:Entity<int>
{
    [MaxLength(500)]
    [Required]
    public string Name { get; set; }
    
    public virtual ICollection<Mine>? Mines { get; set; }
}
