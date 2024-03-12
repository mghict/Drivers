using Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class CityRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<City, int>, ICityRepository
{
    public CityRepository(DbContext context) : base(context)
    {
    }
}
