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
public class AutoService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public AutoService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task<AutoDetailsDto?> GetAutoDetailsAsync(long deviceCode)
    {
        var auto = await _uw.AutoRepository
                            .FirstOrDefaultAsync(filter : p=>p.DeviceCode==deviceCode,
                                                 include: p=>p.Include(p=>p.Document!).ThenInclude(p=>p.Content)
                                                              .Include(p=>p.Person).ThenInclude(p=>p.Document!).ThenInclude(p=>p.Content) 
                                                              .Include(p=>p.AutoModel).ThenInclude(p=>p.AutoBrand)
                                                              .Include(p=>p.Mine).ThenInclude(p=>p.Location).ThenInclude(p=>p.City).ThenInclude(p=>p.Province)
                                                 );

        return _mp.Map<AutoDetailsDto>(auto);
    }
    public async Task<DataResult<AutoDto>> GetAutosPagableAsync(DataRequest request)
    {
        var autos = await _uw.AutoRepository.ReadPagableAsync(request,
                                                               include:p=>p.Include(p=>p.Mine!)
                                                                           .ThenInclude(p=>p.Location!)
                                                                           .Include(p=>p.Person!)
                                                                           .Include(p=>p.AutoModel!)
                                                                           .ThenInclude(p=>p.AutoBrand!)
                                                               );

        return _mp.MapDataResult<Auto, AutoDto>(autos);
    }

    public async Task<DataResult<AutoDto>> GetAutosPagableAsync(DataRequest request,User user)
    {
        var autos = await _uw.AutoRepository.GetAutosPagableAsync(request,user);
        return _mp.MapDataResult<Auto, AutoDto>(autos);
    }

    public async Task<AutoLastLocationModel> GetAutoLastLocationAsync(long id)
    {
        var auto = await _uw.RecievedWeightRepository
                             .FirstOrDefaultAsync(filter:p=>p.AutoId==id,
                                                  orderBy: p=>p.OrderByDescending(p=>p.MissionCode).ThenByDescending(p => p.SendDate).ThenByDescending(p=>p.RowNumber),
                                                  include: p=>p.Include(p=>p.RecievedMission!)
                                                               .Include(p=>p.RecievedSpeedAndTempratures!));

        var autoEntity = await _uw.AutoRepository.FirstOrDefaultAsync(filter: p => p.Id == id,
                                                                      include: p=>p.Include(p=>p.Person)
                                                                                   .Include(p=>p.Mine));
        if (autoEntity is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        if (auto is null)
        {
            return new AutoLastLocationModel()
            {
                Id = id,
                Lat = null,
                Lng = null,
                Auto = _mp.Map<AutoDto>(autoEntity)
            };
        }

        if (auto!.RecievedMission is not null)
            return new AutoLastLocationModel()
            {
                Id = id,
                Lat = auto.RecievedMission.Lat,
                Lng = auto.RecievedMission.Lng,
                Auto=_mp.Map<AutoDto>(autoEntity)
            };

        if(auto!.RecievedSpeedAndTempratures is not null && auto!.RecievedSpeedAndTempratures.Count()>0)
        {
            var temp = auto!.RecievedSpeedAndTempratures.OrderByDescending(p => p.MissionCode)
                                                        .ThenByDescending(p => p.SendDate)
                                                        .ThenByDescending(p => p.RowNumber)
                                                        .FirstOrDefault();
            return new AutoLastLocationModel()
            {
                Id = id,
                Lat = temp!.Lat,
                Lng = temp!.Lng,
                Auto = _mp.Map<AutoDto>(autoEntity)
            };
        }

        return new AutoLastLocationModel()
        {
            Id = id,
            Lat = auto?.Lat ?? null,
            Lng = auto?.Lng ?? null,
            Auto = _mp.Map<AutoDto>(autoEntity)
        };
    }

    public async Task<AutoDto> GetAutoAsync(long id)
    {
        var auto = await _uw.AutoRepository.FirstOrDefaultAsync(filter: p => p.Id == id,
                                                                      include: p => p.Include(p => p.Mine!)
                                                                           .ThenInclude(p => p.Location)
                                                                           .Include(p => p.Person)
                                                                           .Include(p => p.AutoModel)
                                                                           .ThenInclude(p => p.AutoBrand)
                                                                           .Include(p=>p.Document!));
        if (auto is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        return _mp.Map<AutoDto>(auto);
    }

    public async Task CreateAutoAsync(AutoCreateModel model)
    {
        var exists = await _uw.AutoRepository.AnyAsync(filter: p => p.VIN.Trim()==model.VIN.Trim() && p.Pelak.Trim()==model.Pelak.Trim());
        if (exists)
            throw new BizException(BizExceptionCode.DataIsExists);

        var driver = await _uw.PersonRepository.FirstOrDefaultAsync(filter: p => p.PersonCode == model.PersonCode && 
                                                                                 p.IsActive == true && 
                                                                                 p.Type == PersonType.Drivers);

        if (driver is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        var entity = _mp.Map<Auto>(model);
        entity.Person = driver;

        await _uw.AutoRepository.InsertAsync(entity);
        
        await _uw.CommitAsync();
        
    }

    public async Task UpdateAutoAsync(AutoUpdateModel model)
    {
        var exists = await _uw.AutoRepository.AnyAsync(filter: p => p.VIN.Trim() == model.VIN.Trim() && 
                                                                    p.Pelak.Trim() == model.Pelak.Trim() && 
                                                                    p.Id!=model.Id);
        if (exists)
            throw new BizException(BizExceptionCode.DataIsExists);

        var driver = await _uw.PersonRepository.FirstOrDefaultAsync(filter: p => p.PersonCode == model.PersonCode &&
                                                                                 p.IsActive == true &&
                                                                                 p.Type == PersonType.Drivers);

        if (driver is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        var entity = await _uw.AutoRepository.FirstOrDefaultAsync(filter: p => p.Id == model.Id);
        if (entity is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        _mp.Map(model, entity);
        entity.Person = driver;

        await _uw.CommitAsync();
    }

    public async Task DeleteAutoAsync(long id)
    {
        var auto = await _uw.AutoRepository.FirstOrDefaultAsync(filter: p => p.Id == id);
        if (auto is null)
            throw new BizException(BizExceptionCode.DataNotFound);

        await _uw.AutoRepository.DeleteAsync(auto);

        try
        {
            await _uw.CommitAsync();
        }
        catch(Exception ex)
        {
            throw new BizException(BizExceptionCode.General_DeleteNotComplete);
        }
    }


    public async Task<DataResult<AutoMissionsModel>> GetAutosMissionsPagableAsync(DataRequest request)
    {
        var autos = await _uw.RecievedWeightRepository.ReadPagableAsync(request,
                                                               include: p =>p.Include(p=>p.RecievedMission!)
                                                                             .Include(p=>p.Auto)
                                                                             .ThenInclude(p=>p.Mine)
                                                                             .ThenInclude(p=>p.Location)
                                                                             .ThenInclude(p=>p.City)
                                                                             .ThenInclude(p=>p.Province)
                                                                             .Include(p=>p.Auto)
                                                                             .ThenInclude(p=>p.Person),
                                                                orderBy: o=>o.OrderByDescending(p=>p.SendDate)
                                                                             .ThenByDescending(p=>p.RowNumber)
                                                               );

        return _mp.MapDataResult<RecievedWeight, AutoMissionsModel>(autos);
    }

    public async Task<DataResult<AutoMissionsModel>> GetAutosMissionsPagableAsync(DataRequest request,User user)
    {
        var provinceIds = user.Provinces?.Select(p => p.Id)?.ToList();
        var mineIds = user.Mines?.Select(p => p.Id)?.ToList();

        var autos = user.RoleId switch
        {
            3=> await GetProvince(request,provinceIds),
            4=> await GetMine(request,mineIds),
            _ => await GetAdmin(request)
        };

        return _mp.MapDataResult<RecievedWeight, AutoMissionsModel>(autos);
    }

    public async Task<DataResult<AutoTransportModel>> GetAutosTransportPagableAsync(DataRequest request,long id)
    {
        
        var details = await _uw.RecievedSpeedAndTempratureRepository
                             .ReadPagableAsync(request,
                                               filter:p=>p.RecievedWeightId==id,
                                               orderBy: o=>o.OrderBy(p=>p.SendDate).ThenBy(p=>p.RowNumber));

        return _mp.MapDataResult<RecievedSpeedAndTemprature, AutoTransportModel>(details);
    }

    public async Task<IEnumerable<AutoTransportModel>> GetAutosTransportListAsync( long id)
    {
        var start = await _uw.RecievedWeightRepository.FirstOrDefaultAsync(filter:p=>p.Id==id);
        var end = await _uw.RecievedMissionRepository.FirstOrDefaultAsync(filter: p => p.RecievedWeightId == id);
        var details = (await _uw.RecievedSpeedAndTempratureRepository
                             .ReadAsync(    filter: p => p.RecievedWeightId == id &&
                                                         p.Lat!=0 && p.Lng!=0 &&
                                                         p.IsReturn==false,
                                            orderBy: o => o.OrderBy(p => p.SendDate).ThenBy(p => p.RowNumber)))?.ToList();

        List<AutoTransportModel> all = new List<AutoTransportModel>();

        if (start is not null)
        {
            all.Add(new AutoTransportModel
            {
                DeviceCode = start.DeviceCode,
                Lat = start.Lat,
                Lng = start.Lng,
                MissionCode = start.MissionCode,
                SendDate = start.SendDate,
                Speed = 0,
                Temprature = 0
            });
        }

        if (details is not null)
        {
            foreach (var item in details)
            {
                //if (end is not null && item.SendDate >= end.SendDate)
                //    continue;


                //if (end is not null && item.RowNumber >= end.RowNumber && item.SendDate >= end.SendDate)
                //    continue;

                all.Add(new AutoTransportModel
                {
                    DeviceCode = item.DeviceCode,
                    Lat = item.Lat,
                    Lng = item.Lng,
                    MissionCode = item.MissionCode,
                    SendDate = item.SendDate,
                    Speed = item.Speed,
                    Temprature = item.Temprature
                });
            }
        }

        

        if (end is not null)
        {
            all.Add(new AutoTransportModel
            {
                DeviceCode = end.DeviceCode,
                Lat = end.Lat,
                Lng = end.Lng,
                MissionCode = end.MissionCode,
                SendDate = end.SendDate,
                Speed = 0,
                Temprature = 0
            });
        }

        //return _mp.MapCollection<RecievedSpeedAndTemprature, AutoTransportModel>(all.AsEnumerable());
        return all;
    }

    private async Task<DataResult<RecievedWeight>> GetAdmin(DataRequest request)
    {
        return await _uw.RecievedWeightRepository.ReadPagableAsync(request,
                                                               filter:p=>p.HasRecievedMission==true,
                                                               include: p => p.Include(p => p.RecievedMission!)
                                                                             .Include(p => p.Auto!)
                                                                             .ThenInclude(p => p.Mine)
                                                                             .ThenInclude(p => p.Location)
                                                                             .ThenInclude(p => p.City)
                                                                             .ThenInclude(p => p.Province)
                                                                             .Include(p => p.Auto!)
                                                                             .ThenInclude(p => p.Person)
                                                                             .Include(p => p.Material!),
                                                                orderBy: o => o.OrderByDescending(p => p.SendDate)
                                                                             .ThenByDescending(p => p.RowNumber)
                                                               );
    }

    private async Task<DataResult<RecievedWeight>> GetProvince(DataRequest request,List<int>? provinceIds)
    {
        return await _uw.RecievedWeightRepository.ReadPagableAsync(request,
                                                               filter: p=> provinceIds!.Contains(p.Auto!.Mine.Location.City.ProvinceId) &&
                                                                           p.HasRecievedMission == true,
                                                               include: p => p.Include(p => p.RecievedMission!)
                                                                             .Include(p => p.Auto!)
                                                                             .ThenInclude(p => p.Mine)
                                                                             .ThenInclude(p => p.Location)
                                                                             .ThenInclude(p => p.City)
                                                                             .ThenInclude(p => p.Province)
                                                                             .Include(p => p.Auto!)
                                                                             .ThenInclude(p => p.Person)
                                                                             .Include(p=>p.Material!),
                                                                orderBy: o => o.OrderByDescending(p => p.SendDate)
                                                                             .ThenByDescending(p => p.RowNumber)
                                                               );
    }

    private async Task<DataResult<RecievedWeight>> GetMine(DataRequest request, List<long>? mineIds)
    {
        return await _uw.RecievedWeightRepository.ReadPagableAsync(request,
                                                               filter: p => mineIds!.Contains(p.Auto!.MineId) &&
                                                                            p.HasRecievedMission == true,
                                                               include: p => p.Include(p => p.RecievedMission!)
                                                                             .Include(p => p.Auto)
                                                                             .ThenInclude(p => p.Mine)
                                                                             .ThenInclude(p => p.Location)
                                                                             .ThenInclude(p => p.City)
                                                                             .ThenInclude(p => p.Province)
                                                                             .Include(p => p.Auto!)
                                                                             .ThenInclude(p => p.Person!)
                                                                             .Include(p => p.Material!),
                                                                orderBy: o => o.OrderByDescending(p => p.SendDate)
                                                                             .ThenByDescending(p => p.RowNumber)
                                                               );
    }
}
