using Driver.API.Common;
using Driver.Common.Models;
using Driver.Domain.Entities;
using Driver.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Driver.API.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController : AppBaseController
    {
        private readonly DocumentService documentService;


        public DocumentsController(
            IHttpContextAccessor contextAccessor,
            DocumentService documentService
            )
            : base(contextAccessor)
        {
            this.documentService = documentService;
        }

        [HttpGet]
        [Route("{documentId}", Name = "userDocumentDownload")]
        //[JWTAuthorization()]
        public async Task<FileContentResult> DownloadDocument(string documentId, CancellationToken cancellationToken = default)
        {
            var guid = Guid.Parse(documentId);
            var result = await documentService.ReadDocumentAsync(guid, cancellationToken);
            return File(result.Content, result.ContentType, result.OriginalFileName, false);
        }


        [HttpPost()]
        [Route("image-upload")]
        [JWTAuthorization()]
        public async Task<DocumentDto> CreateReceiptImagePurches([FromForm] DocumentCreateDto dto)
        {

            var doc = await documentService.UpsertDocumentAsync(dto);
            doc.SetUserScopeUrls(Url);
            return doc;
        }
    }
}
