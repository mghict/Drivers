using DocumentFormat.OpenXml.Wordprocessing;
using Driver.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("AutoDefinations")]
[Index(nameof(DeviceCode), Name = "IX_Auto_DeviceCode")]
[Index(nameof(VIN), Name = "IX_Auto_VIN")]
[Index(nameof(Pelak), Name = "IX_Auto_Pelak")]
public class Auto:Entity<long>
{
    public int AutoModelId { get; set; }
    public AutoModel AutoModel { get; set; }
    public int CreateYear { get; set; }
    public int Weight { get; set; } = 0;

    [MaxLength(30)]
    public string Pelak { get; set; }
    public string VIN { get; set; }
    public long PersonId { get; set; }
    public Person Person { get; set; }

    public long MineId { get; set; }
    public Mine Mine { get; set; }

    public bool IsActive { get; set; } = true;

    public long DeviceCode { get; set; } = 0L;
    
    public long? DocumentId { get; set; }
    
    [ForeignKey("DocumentId")]
    public Driver.Domain.Entities.Document? Document { get; set; }
    public virtual ICollection<RecievedError>? RecievedErrors { get; set; }
    public virtual ICollection<RecievedSpeedAndTemprature>? RecievedSpeedAndTempratures { get; set; }
    public virtual ICollection<RecievedWeight>? RecievedWeights { get; set; }
    public virtual ICollection<RecievedMission>? RecievedMissions { get; set; }
    public virtual ICollection<RecievedNumber>? RecievedNumbers { get; set; }

}
