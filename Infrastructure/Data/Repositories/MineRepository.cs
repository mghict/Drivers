using Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class MineRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<Mine, long>, IMineRepository
{
    public MineRepository(DbContext context) : base(context)
    {
    }
}
