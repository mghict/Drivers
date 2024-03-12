using Domain.Entities;
using Moneyon.Common.Data;

namespace Driver.Service.IRepositories;

public interface ICityRepository : IGenericRelationalRepository<City, int>
{
}
