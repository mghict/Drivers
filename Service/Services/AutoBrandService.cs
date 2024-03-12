using AutoMapper;
using Common.Extensions;
using Domain.Entities;
using Driver.Common;
using Driver.Common.Models;
using Driver.Service.IRepositories;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;

namespace Driver.Service.Services;

[AutoRegister()]
public class AutoBrandService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public AutoBrandService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task<DataResult<AutoBrandModel>> GetBrandsPagableAsync(DataRequest request)
    {
        var brands=await _uw.AutoBrandRepository.ReadPagableAsync(request);
        return _mp.MapDataResult<AutoBrand, AutoBrandModel>(brands);
    }

    public async Task<IEnumerable<AutoBrandModel>> GetBrandsAsync()
    {
        var brands = await _uw.AutoBrandRepository.ReadAsync();
        return _mp.MapCollection<AutoBrand, AutoBrandModel>(brands);
    }

    public async Task<AutoBrandModel> GetBrandAsync(int id)
    {
        var brand = await _uw.AutoBrandRepository.FirstOrDefaultAsync(filter: p=>p.Id==id);
        if (brand is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        return _mp.Map<AutoBrandModel>(brand);
    }

    public async Task CreateBrandAsync(AutoBrandModel model)
    {
        var exists = await _uw.AutoBrandRepository.AnyAsync(filter: p => p.Name.ToLower().Trim() == model.Name.ToLower().Trim());
        if (exists)
            throw new BizException(BizExceptionCode.NameIsExists);

        var entity = _mp.Map<AutoBrand>(model);
        await _uw.AutoBrandRepository.InsertAsync(entity);
        await _uw.CommitAsync();
    }

    public async Task UpdateBrandAsync(AutoBrandModel model)
    {
        var exists = await _uw.AutoBrandRepository.AnyAsync(filter: p => p.Name.ToLower().Trim() == model.Name.ToLower().Trim() && p.Id!=model.Id);
        if (exists)
            throw new BizException(BizExceptionCode.NameIsExists);

        var entity = await _uw.AutoBrandRepository.FirstOrDefaultAsync(filter: p => p.Id == model.Id);
        if(entity is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        _mp.Map(model,entity);
        await _uw.CommitAsync();
    }

    public async Task DeleteBrandAsync(int id)
    {
        var entity = await _uw.AutoBrandRepository.FirstOrDefaultAsync(filter: p => p.Id == id);
        if (entity is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        var exists = await _uw.AutoModelRepository.AnyAsync(filter: p => p.AutoBrandId == id);
        if (exists)
            throw new BizException(BizExceptionCode.General_DeleteNotComplete);

        await _uw.AutoBrandRepository.DeleteAsync(entity);
        await _uw.CommitAsync();
    }
}
