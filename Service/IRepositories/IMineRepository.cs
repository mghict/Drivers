using Domain.Entities;
using Moneyon.Common.Data;

namespace Driver.Service.IRepositories;

public interface IMineRepository : IGenericRelationalRepository<Mine, long>
{
}
