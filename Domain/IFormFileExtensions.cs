using Driver.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Domain;

public static class IFormFileExtensions
{
    public static async Task<Document> ToDocumentAsync(this IFormFile formFile)
    {
        byte[] content;
        using (var ms = new MemoryStream())
        {
            await formFile.CopyToAsync(ms);
            content = ms.ToArray();
        }

        return new Document(formFile.FileName, formFile.ContentType, content)
        {
        };
    }

    public static async Task<Tuple<string, string, byte[]>> ToDocumentValuesAsync(this IFormFile formFile)
    {
        byte[] content;
        using (var ms = new MemoryStream())
        {
            await formFile.CopyToAsync(ms);
            content = ms.ToArray();
        }

        return new Tuple<string, string, byte[]>(formFile.FileName, formFile.ContentType, content);
    }
}
