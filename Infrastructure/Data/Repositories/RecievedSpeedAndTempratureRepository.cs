using Driver.Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class RecievedSpeedAndTempratureRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<RecievedSpeedAndTemprature, long>, IRecievedSpeedAndTempratureRepository
{
    public RecievedSpeedAndTempratureRepository(DbContext context) : base(context)
    {
    }
}
