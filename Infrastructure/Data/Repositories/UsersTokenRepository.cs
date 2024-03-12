using Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class UsersTokenRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<UsersToken, long>, IUsersTokenRepository
{
    public UsersTokenRepository(DbContext context) : base(context)
    {
    }
}
