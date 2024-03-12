using Domain.Entities;
using Driver.Common.Models;
using Moneyon.Common.Data;

namespace Driver.Service.IRepositories;

public interface IAutoRepository : IGenericRelationalRepository<Auto, long>
{
    Task<DataResult<Auto>> GetAutosPagableAsync(DataRequest request, User user);
}




