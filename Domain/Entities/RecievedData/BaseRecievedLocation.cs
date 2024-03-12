using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations.Schema;

namespace Driver.Domain.Entities;

public class BaseRecievedLocation : BaseRecieved
{
    [Column(TypeName = "decimal(12,8)")]
    public decimal Lat { get; set; }

    [Column(TypeName = "decimal(12,8)")]
    public decimal Lng { get; set; }

    public bool IsValidAddress { get; set; } = false;
    public long? MapReverseId { get;set; }
    public MapReverse? MapReverse { get; set; }
}
