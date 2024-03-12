using AutoMapper;
using Common.Extensions;
using Domain.Entities;
using Driver.Common;
using Driver.Common.Models;
using Driver.Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using Moneyon.Common.ExceptionHandling;
using Moneyon.Common.IOC;

namespace Driver.Service.Services;

[AutoRegister()]
public class MaterialService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public MaterialService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }


    public async Task<DataResult<MaterialModel>> GetMaterialPagableAsync(DataRequest request)
    {
        var materials = await _uw.MaterialRepository.ReadPagableAsync(request);
        return _mp.MapDataResult<Material, MaterialModel>(materials);
    }
    public async Task<IEnumerable<MaterialModel>> GetMaterialsAsync()
    {
        var materials = await _uw.MaterialRepository.ReadAsync();
        return _mp.MapCollection<Material, MaterialModel>(materials);
    }
    public async Task<IEnumerable<MaterialModel>> GetMaterialsByMineAsync(long mineId)
    {
        var materials = (await _uw.MineRepository.FirstOrDefaultAsync(filter: p=>p.Id==mineId,
                                                                     include: p=>p.Include(p=>p.Materials!)))?.Materials;
        return _mp.MapCollection<Material, MaterialModel>(materials);
    }

    public async Task<IEnumerable<MaterialModel>> GetMaterialsByMineCodeAsync(long mineCode)
    {
        var materials = (await _uw.MineRepository.FirstOrDefaultAsync(filter: p => p.MineCode == mineCode,
                                                                     include: p => p.Include(p => p.Materials!)))?.Materials;
        return _mp.MapCollection<Material, MaterialModel>(materials);
    }
    public async Task<MaterialModel> GetMaterialAsync(int id)
    {
        var material = await _uw.MaterialRepository.FirstOrDefaultAsync(p=>p.Id==id);
        if (material is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        return _mp.Map<MaterialModel>(material);
    }
    public async Task CreateMaterialAsync(MaterialModel model)
    {
        var existsName = await _uw.MaterialRepository.AnyAsync(p => p.Name.Trim().ToLower()==model.Name.Trim().ToLower());
        if (existsName)
            throw new BizException(BizExceptionCode.DataIsExists);

        var material = _mp.Map<Material>(model);
        await _uw.MaterialRepository.InsertAsync(material);
        await _uw.CommitAsync();
    }
    public async Task UpdateMaterialAsync(MaterialModel model)
    {
        var existsName = await _uw.MaterialRepository.AnyAsync(p => p.Name.Trim().ToLower() == model.Name.Trim().ToLower() &&
                                                                    p.Id!=model.Id);
        if (existsName)
            throw new BizException(BizExceptionCode.DataIsExists);

        var material = await _uw.MaterialRepository.FirstOrDefaultAsync(filter:p => p.Id == model.Id);
        _mp.Map(model,material);
        //material!.Name = model.Name.Trim();

        await _uw.CommitAsync();
    }

    public async Task DeleteMaterialAsync(int materialId)
    {

        var material = await _uw.MaterialRepository.FirstOrDefaultAsync(filter: p => p.Id == materialId);
        if (material is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        await _uw.MaterialRepository.DeleteAsync(material);

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