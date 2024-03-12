using Driver.Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class RecievedNumberRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<RecievedNumber, long>, IRecievedNumberRepository
{
    public RecievedNumberRepository(DbContext context) : base(context)
    {
    }
}
