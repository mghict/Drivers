using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Driver.Domain.Entities;

[Table("Recieved_Error")]
[Index(nameof(DeviceCode), Name = "IX_RecievedError_DeviceCode")]
[Index(nameof(RowNumber), Name = "IX_RecievedError_RowNumber")]
[Index(nameof(AutoId), Name = "IX_RecievedError_AutoId")]
[Index(nameof(SendDate), Name = "IX_RecievedError_SendDate")]
public class RecievedError : BaseRecieved
{
    public long ErrorCode { get; set; }
}
