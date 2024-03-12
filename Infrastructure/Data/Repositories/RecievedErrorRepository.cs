using Driver.Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class RecievedErrorRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<RecievedError, long>, IRecievedErrorRepository
{
    public RecievedErrorRepository(DbContext context) : base(context)
    {
    }
}
