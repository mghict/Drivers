using Driver.Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class MapReverseRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<MapReverse, long>, IMapReverseRepository
{
    public MapReverseRepository(DbContext context) : base(context)
    {
    }
}
