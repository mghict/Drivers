using Driver.Common.Models;

namespace Driver.Domain.Entities;

public class DocumentWithContentDto : DocumentDto
{
    public byte[] Content { get; set; }
}
