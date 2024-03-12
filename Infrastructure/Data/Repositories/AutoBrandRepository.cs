using Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class AutoBrandRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<AutoBrand, int>, IAutoBrandRepository
{
    public AutoBrandRepository(DbContext context) : base(context)
    {
    }
}
