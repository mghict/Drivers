using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Common.Models;

public class DocumentDto
{
    public DocumentType Type { get; set; }
    public Guid Guid { get; set; }
    public string OriginalFileName { get; set; }
    public string FileName => OriginalFileName;
    public string ContentType { get; set; }
    public string? Url { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
}


public class DocumentDetailDto
{
    public DocumentType Type { get; set; }
    public Guid Guid { get; set; }
    public byte[]? Value { get; set; }
}
