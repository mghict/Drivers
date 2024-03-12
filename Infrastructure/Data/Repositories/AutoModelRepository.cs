using Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class AutoModelRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<AutoModel, int>, IAutoModelRepository
{
    public AutoModelRepository(DbContext context) : base(context)
    {
    }
}
