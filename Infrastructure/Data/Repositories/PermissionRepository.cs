using Driver.Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class PermissionRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<Permission, int>, IPermissionRepository
{
    public PermissionRepository(DbContext context) : base(context)
    {
    }
}