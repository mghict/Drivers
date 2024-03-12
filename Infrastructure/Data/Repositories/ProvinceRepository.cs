using Domain.Entities;
using Driver.Service.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class ProvinceRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<Province, int>, IProvinceRepository
{
    public ProvinceRepository(DbContext context) : base(context)
    {
    }
}
