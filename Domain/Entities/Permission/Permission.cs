using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Domain.Entities;

[Index(nameof(PermisionId), Name = "IX_Permissio_PermisionId")]
public class Permission:Entity<int>
{
    public int PermisionId { get; set; }

    [MaxLength(150)]
    public string Name { get; set; }

    [MaxLength(250)]
    public string? Description { get; set; }

    public virtual ICollection<Role>? Roles { get; set; }
}
