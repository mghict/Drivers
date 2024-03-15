using Domain.Entities;
using Driver.Domain.Entities;
using Moneyon.Common.Data;

namespace Driver.Service.IRepositories;

public interface IRecievedErrorRepository : IGenericRelationalRepository<RecievedError, long>
{
    Task<DataResult<RecievedError>> GetAutoErrorsPagableAsync(DataRequest request, long autoId);
    Task<DataResult<RecievedError>> GetAutosByErrorCodePagableAsync(DataRequest request, int errorCode,User user);
    Task<DataResult<RecievedError>> GetAutosByErrorCodePagableAsync(DataRequest request, int errorCode);
}



