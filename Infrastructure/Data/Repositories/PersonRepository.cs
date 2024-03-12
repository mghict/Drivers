using Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Driver.Infrastructure.Data.Repositories;

public class PersonRepository : Moneyon.Common.Data.SqlServer.SqlServerGenericRepository<Person, long>, IPersonRepository
{
    public PersonRepository(DbContext context) : base(context)
    {
    }
}
