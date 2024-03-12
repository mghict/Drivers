using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Driver.Domain.Entities;

[Table("Recieved_SpeedAndTemprature")]
[Index(nameof(DeviceCode), Name = "IX_RecievedSpeedAndTemprature_DeviceCode")]
[Index(nameof(RowNumber), Name = "IX_RecievedSpeedAndTemprature_RowNumber")]
[Index(nameof(AutoId), Name = "IX_RecievedSpeedAndTempraturer_AutoId")]
[Index(nameof(SendDate), Name = "IX_RecievedSpeedAndTemprature_SendDate")]
[Index(nameof(MissionCode), Name = "IX_RecievedSpeedAndTemprature_MissionCode")]
[Index(nameof(RecievedWeightId), Name = "IX_RecievedSpeedAndTemprature_RecievedWeightId")]
public class RecievedSpeedAndTemprature : BaseRecievedLocation
{

    [Column(TypeName = "decimal(12,4)")]
    public decimal Temprature { get; set; }

    [Column(TypeName = "decimal(12,2)")]
    public decimal Speed { get; set; }

    public long MissionCode { get; set; }

    public long? RecievedWeightId { get; set; }
    public RecievedWeight? RecievedWeight { get; set; }
    public bool IsReturn { get; set; } = false;
}
