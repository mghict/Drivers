using Driver.Domain.Entities;
using Moneyon.Common.Data;

namespace Driver.Service.IRepositories;

public interface IRecievedErrorRepository : IGenericRelationalRepository<RecievedError, long>
{
    Task<DataResult<RecievedError>> GetAutoErrorsPagableAsync(DataRequest request, long autoId);
}



