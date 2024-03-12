using Domain.Entities;
using Driver.Common.Models.Reports;
using Driver.Domain.Entities;
using Moneyon.Common.Data;

namespace Driver.Service.IRepositories;
public interface IRecievedWeightRepository : IGenericRelationalRepository<RecievedWeight, long>
{
    Task<DataResult<CitySumModel>> GetCitySumAsync(DataRequest request, int provinceId, CancellationToken cancellationToken = default);
    Task<DataResult<CitySumModel>> GetCitySumAsync(DataRequest request, CancellationToken cancellationToken = default);
    Task<DataResult<CitySumModel>> GetCitySumAsync(DataRequest request,User user, CancellationToken cancellationToken = default);
    Task<DataResult<MineMaterialSumModel>> GetMineMaterialSumAsync(DataRequest request, long mineId, User user, CancellationToken cancellationToken = default);
    Task<DataResult<MineSumModel>> GetMineSumAsync(DataRequest request, int cityId, CancellationToken cancellationToken = default);
    Task<DataResult<MineSumModel>> GetMineSumAsync(DataRequest request, CancellationToken cancellationToken = default);
    Task<DataResult<ProvinceMineSumModel>> GetMineSumAsync(DataRequest request, User user, CancellationToken cancellationToken = default);
    Task<DataResult<ProvinceMineSumModel>> GetProvinceMineSumAsync(DataRequest request, int provinceId, User user, CancellationToken cancellationToken = default);

    Task<DataResult<ProvinceSumModel>> GetProvinceSumAsync(DataRequest request, CancellationToken cancellationToken = default);
    Task<DataResult<ProvinceSumModel>> GetProvinceSumAsync(DataRequest request,User user, CancellationToken cancellationToken = default);
}



