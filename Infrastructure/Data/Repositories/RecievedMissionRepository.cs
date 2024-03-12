using Driver.Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class RecievedMissionRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<RecievedMission, long>, IRecievedMissionRepository
{
    public RecievedMissionRepository(DbContext context) : base(context)
    {
    }
}
