using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Driver.Domain.Entities;

[Table("Recieved_Number")]
[Index(nameof(DeviceCode), Name = "IX_RecievedNumber_DeviceCode")]
[Index(nameof(RowNumber), Name = "IX_RecievedNumber_RowNumber")]
[Index(nameof(AutoId), Name = "IX_RecievedNumberr_AutoId")]
[Index(nameof(SendDate), Name = "IX_RecievedNumber_SendDate")]
public class RecievedNumber: BaseRecievedLocation
{

    [Column(TypeName ="varchar(20)")]
    public string MobileNumber { get; set; }
}