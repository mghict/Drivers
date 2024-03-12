using Domain.Entities;
using Domain.Interface;
using Driver.Common.Models;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;

namespace Driver.Domain.Entities;

/// <summary>
/// سند
/// </summary>

[Index(nameof(Guid), Name = "IX_Document_Guid")]
public class Document : Entity<long>, IAudit
{
    
    public Guid Guid { get; set; }
    public DocumentType Type { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

    /// <summary>
    /// In Bytes
    /// </summary>
    public long Size { get; set; }
    public string OriginalFileName { get; set; }
    public string ContentType { get; set; }
    public DocumentContent Content { get; set; }

    public long? PersonId { get; set; }
    public Person? Person { get; set; }

    public long? AutoId { get; set; }
    public Auto? Auto { get; set; }

    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }


    public Document()
    {
        Guid = Guid.NewGuid();
    }


    public Document(string originalFileName, string contentType, byte[] content)
        : this()
    {
        OriginalFileName = originalFileName;
        ContentType = contentType;
        Size = content.Length;
        Content = new DocumentContent() { Value = content };
    }

    public Document(DocumentType type, string originalFileName, string contentType, byte[] content)
        : this(originalFileName, contentType, content)
    {
        Type = type;
    }
}
