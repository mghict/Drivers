using Driver.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Driver.API.Common
{
    public static class DocumentDtoExtensions
    {
        //public static void SetAdminScopeUrls(this DocumentDto document, IUrlHelper urlHelper)
        //{
        //    document.Url = urlHelper.RouteUrl("adminDocumentDownload", new { documentId = document.Guid })!;
        //    document.ThumbnailUrl = urlHelper.RouteUrl("adminDocumentDownload", new { documentId = document.Guid })!;
        //}

        public static void SetUserScopeUrls(this DocumentDto document, IUrlHelper urlHelper)
        {
            document.Url = urlHelper.RouteUrl("userDocumentDownload", new { documentId = document.Guid.ToString() })!;
            document.ThumbnailUrl = urlHelper.RouteUrl("userDocumentDownload", new { documentId = document.Guid.ToString() })!;
        }


    }
}
