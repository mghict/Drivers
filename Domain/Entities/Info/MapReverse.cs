using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Moneyon.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Domain.Entities;

[Table("MapReverse",Schema ="dbo")]
[Index("Lat","Lng",IsUnique =true,Name = "IX_LatAndLng_MapReverse")]
public class MapReverse:Entity<long>
{
    [Column(TypeName = "decimal(12,8)")]
    public decimal Lat { get; set; }

    [Column(TypeName = "decimal(12,8)")]
    public decimal Lng { get; set; }

    public string? Address { get; set; }
    public string? PostalAddress { get; set; }
    public string? AddressCompact { get; set; }

    [MaxLength(255)]
    public string? Primary { get; set; }
    
    [MaxLength(255)]
    public string? Name { get; set; }
    
    [MaxLength(255)]
    public string? Poi { get; set; }
    
    [MaxLength(255)]
    public string? Penult { get; set; }
    
    [MaxLength(255)]
    public string? Country { get; set; }
    
    [MaxLength(255)]
    public string? Province { get; set; }
    
    [MaxLength(255)]
    public string? County { get; set; }
    
    [MaxLength(255)]
    public string? District { get; set; }
    
    [MaxLength(255)]
    public string? RuralDistrict { get; set; }
    
    [MaxLength(255)]
    public string? City { get; set; }
    
    [MaxLength(255)]
    public string? Village { get; set; }
    
    [MaxLength(255)]
    public string? Region { get; set; }
    
    [MaxLength(255)]
    public string? Neighbourhood { get; set; }
    
    [MaxLength(255)]
    public string? Last { get; set;}
    
    [MaxLength(255)]
    public string? Plaque { get; set;}
    
    [MaxLength(255)]
    public string? PostalCode { get; set; }

    
}
