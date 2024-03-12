using Domain.Entities;
using Moneyon.Common.Data;

namespace Driver.Service.IRepositories;

public interface IPersonRepository : IGenericRelationalRepository<Person, long>
{
}

