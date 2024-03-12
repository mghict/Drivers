using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Driver.Domain.Entities;


[Table("Recieved_FinishedMission")]
[Index(nameof(DeviceCode), Name = "IX_RecievedMission_DeviceCode")]
[Index(nameof(RowNumber), Name = "IX_RecievedMission_RowNumber")]
[Index(nameof(AutoId), Name = "IX_RecievedMission_AutoId")]
[Index(nameof(SendDate), Name = "IX_RecievedMission_SendDate")]
[Index(nameof(MissionCode), Name = "IX_RecievedMission_MissionCode")]
[Index(nameof(RecievedWeightId), Name = "IX_RecievedMission_RecievedWeightId")]
public class RecievedMission : BaseRecievedLocation
{
    public long MissionCode { get; set; }
    public long? RecievedWeightId { get; set; }
    public RecievedWeight? RecievedWeight { get; set; }

}
