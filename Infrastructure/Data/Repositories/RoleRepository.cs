using Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class RoleRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<Role, int>, IRoleRepository
{
    public RoleRepository(DbContext context) : base(context)
    {
    }
}
