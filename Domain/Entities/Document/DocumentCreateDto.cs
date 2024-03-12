using Driver.Common.Models;
using Microsoft.AspNetCore.Http;
using Moneyon.Common.Web.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Driver.Domain.Entities;

public class DocumentCreateDto
{
    public Guid? PersonCode { get; set; }
    public long? AutoId { get; set; }
    public DocumentType Type { get; set; }

    [Required(ErrorMessage = "تصویر الزامی است")]
    [DataType(DataType.Upload)]
    [MaxFileSize(100 * 1024 ,ErrorMessage ="تصویر نمیتواند بیشتر از 100 کیلوبایت باشد")]
    [AllowedExtensions(new string[] { ".jpeg", ".jpg", ".png"})]
    public IFormFile File { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }

    public virtual async Task<Document> ToDocumentAsync()
    {
        var doc = await File.ToDocumentAsync();
        doc.Type = Type;
        doc.Title = Title;
        doc.Description = Description;
        doc.AutoId = AutoId ?? null;
        
        return doc;
    }

}
