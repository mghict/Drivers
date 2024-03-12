using Driver.Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class MaterialRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<Material, int>, IMaterialRepository
{
    public MaterialRepository(DbContext context) : base(context)
    {
    }
}
