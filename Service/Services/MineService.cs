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
public class MineService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public MineService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task CreateMineAsync(MineCreateModel model)
    {
        var mine = await _uw.MineRepository.FirstOrDefaultAsync(p => p.Name.ToLower().Trim()==model.Name.ToLower().Trim() || 
                                                                     p.MineCode==model.MineCode);

        if (mine is not null)
            throw new BizException(BizExceptionCode.NameIsExists);

        var materials = await _uw.MaterialRepository.ReadAsync(filter: p => model!.MaterialIds!.Contains(p.Id));

        mine = new Mine()
        {
            Name = model.Name,
            Address = model.Address,
            Location = new Location()
            {
                CityId = model.Location.CityId,
                Name = model.Name,
                lat = model.Location.lat,
                lng = model.Location.lng
            },
            Materials=  new List<Material>() ,
            MineCode=model.MineCode
        };

        mine.Materials = materials?.ToList();

        await _uw.MineRepository.InsertAsync(mine);
        await _uw.CommitAsync();
    }

    public async Task<MineModel> GetMineAsync(long id)
    {
        var mine = await _uw.MineRepository
                            .FirstOrDefaultAsync(filter:p => p.Id == id,
                                                 include:p=>p.Include(p => p.Location)
                                                             .ThenInclude(p => p.City)
                                                             .ThenInclude(p => p.Province)
                                                             .Include(p=>p.Materials!));

        if (mine is  null)
            throw new BizException(BizExceptionCode.DataNotFound);

        return _mp.Map<MineModel>(mine);
    }

    public async Task<DataResult<MineShortModel>> GetMineListAsync(DataRequest request)
    {
        var mineResult = await _uw.MineRepository.ReadPagableAsync(request, include: p => p.Include(p => p.Location!)
                                                                      .ThenInclude(p => p.City!)
                                                                      .ThenInclude(p => p.Province!)
                            );

        return _mp.MapDataResult<Mine, MineShortModel>(mineResult);
    }

    public async Task<IEnumerable<MineShortModel>> GetListAsync()
    {
        var mineResult = await _uw.MineRepository
                            .ReadAsync(include: p => p.Include(p => p.Location!)
                                                                      .ThenInclude(p => p.City!)
                                                                      .ThenInclude(p => p.Province!)
                            );

        return _mp.MapCollection<Mine, MineShortModel>(mineResult);
    }
    
    public async Task UpdateMineAsync(MineUpdateModel model)
    {
        var mineExists = await _uw.MineRepository.AnyAsync(p => ( p.Name.ToLower().Trim() == model.Name.ToLower().Trim() ||
                                                                  p.MineCode==model.MineCode
                                                                )
                                                                && p.Id!=model.Id);

        if (mineExists)
            throw new BizException(BizExceptionCode.NameIsExists);

        var mineEntity = await _uw.MineRepository.FirstOrDefaultAsync(filter:p => p.Id == model.Id,
                                                            include:p=>p.Include(p => p.Location)
                                                                        .Include(p=>p.Materials!));

        if (mineEntity is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        var materials = await _uw.MaterialRepository.ReadAsync(filter: p => model!.MaterialIds!.Contains(p.Id));

        _mp.Map(model, mineEntity);
        mineEntity.Location.Name = model.Name;

        mineEntity.Materials = new List<Material>();
        mineEntity.Materials = materials.ToList();

        try
        {
            await _uw.CommitAsync();
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    public async Task DeleteMineAsync(long mineId)
    {
        var mineEntity = await _uw.MineRepository.FirstOrDefaultAsync(filter: p => p.Id == mineId);

        if (mineEntity is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        await _uw.MineRepository.DeleteAsync(mineEntity);

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
