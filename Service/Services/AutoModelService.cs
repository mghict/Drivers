using AutoMapper;
using Common.Extensions;
using Domain.Entities;
using Driver.Common;
using Driver.Common.Models;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;

namespace Driver.Service.Services;

[AutoRegister()]
public class AutoModelService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public AutoModelService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task<DataResult<AutoModelsModel>> GetModelsPagableAsync(DataRequest request)
    {
        var models = await _uw.AutoModelRepository.ReadPagableAsync(request,
                                                                    include:p =>p.Include(p=>p.AutoBrand));
        return _mp.MapDataResult<AutoModel, AutoModelsModel>(models);
    }

    public async Task<IEnumerable<AutoModelsModel>> GetModelsAsync()
    {
        var models = await _uw.AutoModelRepository.ReadAsync(include: p => p.Include(p => p.AutoBrand));
        return _mp.MapCollection<AutoModel, AutoModelsModel>(models);
    }

    public async Task<IEnumerable<AutoModelsModel>> GetModelsAsync(int brandId)
    {
        var models = await _uw.AutoModelRepository.ReadAsync(filter: p=>p.AutoBrandId==brandId, include: p => p.Include(p => p.AutoBrand));
        return _mp.MapCollection<AutoModel, AutoModelsModel>(models);
    }

    public async Task<AutoModelsModel> GetModelAsync(int id)
    {
        var model = await _uw.AutoModelRepository.FirstOrDefaultAsync(filter: p => p.Id == id,include:p => p.Include(p => p.AutoBrand));
        if (model is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        return _mp.Map<AutoModelsModel>(model);
    }

    public async Task CreateModelAsync(AutoModelsModel model)
    {
        var exists = await _uw.AutoModelRepository.AnyAsync(filter: p => p.Name.ToLower().Trim() == model.Name.ToLower().Trim() && p.AutoBrandId==model.AutoBrandId);
        if (exists)
            throw new BizException(BizExceptionCode.NameIsExists);

        var entity = _mp.Map<AutoModel>(model);
        await _uw.AutoModelRepository.InsertAsync(entity);
        await _uw.CommitAsync();
    }

    public async Task UpdateModelAsync(AutoModelsModel model)
    {
        var exists = await _uw.AutoModelRepository.AnyAsync(filter: p => p.Name.ToLower().Trim() == model.Name.ToLower().Trim() && p.Id != model.Id && p.AutoBrandId==model.AutoBrandId);
        if (exists)
            throw new BizException(BizExceptionCode.NameIsExists);

        var entity = await _uw.AutoModelRepository.FirstOrDefaultAsync(filter: p => p.Id == model.Id, include: p => p.Include(p => p.AutoBrand));
        if (entity is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        _mp.Map(model, entity);
        await _uw.CommitAsync();
    }

    public async Task DeleteModelAsync(int id)
    {
        var entity = await _uw.AutoModelRepository.FirstOrDefaultAsync(filter: p => p.Id == id);
        if (entity is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        var exists = await _uw.AutoRepository.AnyAsync(filter: p => p.AutoModelId == id);
        if (exists)
            throw new BizException(BizExceptionCode.General_DeleteNotComplete);

        await _uw.AutoModelRepository.DeleteAsync(entity);
        try
        {
            await _uw.CommitAsync();
        }
        catch (Exception ex)
        {
            throw new BizException(BizExceptionCode.General_DeleteNotComplete);
        }
    }
}
