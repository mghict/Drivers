using Moneyon.Common.Data;

namespace Driver.Domain.Entities;

/// <summary>
/// محتوای سند
/// </summary>
public class DocumentContent : Entity<long>
{
    public byte[] Value { get; set; }
    public Document Document { get; set; }
}
