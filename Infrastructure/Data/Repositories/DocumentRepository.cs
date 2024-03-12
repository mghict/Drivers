using Driver.Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class DocumentRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<Document, long>, IDocumentRepository
{
    public DocumentRepository(DbContext context) : base(context)
    {
    }
}
