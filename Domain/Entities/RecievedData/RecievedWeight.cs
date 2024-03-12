using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Driver.Domain.Entities;

[Table("Recieved_StartedMission")]
[Index(nameof(DeviceCode), Name = "IX_RecievedWeight_DeviceCode")]
[Index(nameof(RowNumber), Name = "IX_RecievedWeight_RowNumber")]
[Index(nameof(AutoId), Name = "IX_RecievedWeight_AutoId")]
[Index(nameof(SendDate), Name = "IX_RecievedWeight_SendDate")]
[Index(nameof(MissionCode), Name = "IX_RecievedWeight_MissionCode")]
public class RecievedWeight: BaseRecievedLocation
{

    //[Column(TypeName = "decimal(20,2)")]
    public int Weight { get; set; }
    public int Type { get; set; }
    public int? MaterialId { get; set; }
    public Material? Material { get; set; }
    public long MissionCode { get; set; }

    //public long? RecievedMissionId { get; set; }
    public RecievedMission? RecievedMission { get; set; }
    public bool HasRecievedMission { get; set; } = false;
    public virtual ICollection<RecievedSpeedAndTemprature>? RecievedSpeedAndTempratures { get; set; }

}
