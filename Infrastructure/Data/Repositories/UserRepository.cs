using Domain.Entities;
using Driver.Service.IRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver.Infrastructure.Data.Repositories;

public class UserRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<User, long>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }
}
