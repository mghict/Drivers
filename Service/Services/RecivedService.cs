using AutoMapper;
using Driver.Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.EntityFrameworkCore;
using Moneyon.Common.Data;
using Moneyon.Common.IOC;
using System.Reflection;

namespace Driver.Service.Services;

[AutoRegister()]
public class RecivedService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;

    public RecivedService(IUnitOfWork uw, IMapper mp)
    {
        _uw = uw;
        _mp = mp;
    }

    public async Task CreateErrorAsync(long deviceCode, int errorCode, long rowNumber, DateTime sendDate)
    {
        var auto = await _uw.AutoRepository.FirstOrDefaultAsync(p => p.DeviceCode == deviceCode);

        var entity = new RecievedError
        {
            AutoId = auto?.Id ?? null,
            ErrorCodeId = errorCode,
            RowNumber = rowNumber,
            SendDate = sendDate,
            DeviceCode = deviceCode,
        };

        await _uw.RecievedErrorRepository.InsertAsync(entity);
        await _uw.CommitAsync();
    }

    public async Task<long> CreateRecievedStartedMissionAsync(long deviceCode, decimal lat, decimal lng, int weight, int typeId, long missionNumber, long rowNumber, DateTime sendDate)
    {
        var auto = await _uw.AutoRepository.FirstOrDefaultAsync(p => p.DeviceCode == deviceCode);
        var material = await _uw.MaterialRepository.FirstOrDefaultAsync(p => p.Id == typeId);

        var entity = new RecievedWeight
        {
            AutoId = auto?.Id ?? null,
            Lat = lat,
            Lng = lng,
            MissionCode = missionNumber,
            MaterialId = material?.Id ?? null,
            Type = typeId,
            Weight = weight,
            RowNumber = rowNumber,
            SendDate = sendDate,
            DeviceCode = deviceCode,
        };

        await _uw.RecievedWeightRepository.InsertAsync(entity);
        await _uw.CommitAsync();

        return entity.Id;
    }

    public async Task<long> CreateRecievedSpeedAndTempratureAsync(long deviceCode, decimal lat, decimal lng, decimal temp, long speed, long missionNumber, long rowNumber, DateTime sendDate,bool isReturn=false)
    {
        //if (lat == lng && lat == 0)
        //    return;

        var auto = await _uw.AutoRepository.FirstOrDefaultAsync(p => p.DeviceCode == deviceCode);
        RecievedWeight? startedMission = await _uw.RecievedWeightRepository.FirstOrDefaultAsync(filter: p => p.DeviceCode == deviceCode &&
                                                                                                             p.MissionCode == missionNumber,
                                                                                                include: p=>p.Include(p=>p.RecievedMission!));

        if (startedMission is not null && startedMission.HasRecievedMission == true)
        {
            if(startedMission!.RecievedMission!.SendDate<sendDate)
                isReturn = true;
        }

        var entity = new RecievedSpeedAndTemprature
        {
            AutoId = auto?.Id ?? null,
            Lat = lat,
            Lng = lng,
            MissionCode = missionNumber,
            RecievedWeightId = startedMission?.Id ?? null,
            Speed = speed,
            Temprature = temp,
            RowNumber = rowNumber,
            SendDate = sendDate,
            DeviceCode = deviceCode,
            IsReturn = isReturn

        };

        await _uw.RecievedSpeedAndTempratureRepository.InsertAsync(entity);
        await _uw.CommitAsync();

        return entity.Id;
    }

    public async Task<long> CreateRecievedFinishedMissionAsync(long deviceCode, decimal lat, decimal lng, long missionNumber, long rowNumber, DateTime sendDate)
    {
        var auto = await _uw.AutoRepository.FirstOrDefaultAsync(filter:p => p.DeviceCode == deviceCode);

        var recievedWeight = await _uw.RecievedWeightRepository.FirstOrDefaultAsync(filter: p => p.DeviceCode == deviceCode &&
                                                                                                 p.MissionCode == missionNumber);

        if(recievedWeight is not null)
            recievedWeight.HasRecievedMission = true;

        var mission = await _uw.RecievedMissionRepository.FirstOrDefaultAsync(filter: p => p.DeviceCode == deviceCode &&
                                                                                           p.MissionCode == missionNumber);
        if (mission is not null)
        {
            mission!.AutoId = auto?.Id ?? null;
            mission!.Lat = lat;
            mission!.Lng = lng;
            mission!.MissionCode = missionNumber;
            mission!.RowNumber = rowNumber;
            mission!.SendDate = sendDate;
            mission!.DeviceCode = deviceCode;
            mission!.RecievedWeightId = recievedWeight?.Id ?? null;            
        }
        else
        {

            mission = new RecievedMission
            {
                AutoId = auto?.Id ?? null,
                Lat = lat,
                Lng = lng,
                MissionCode = missionNumber,
                RowNumber = rowNumber,
                SendDate = sendDate,
                DeviceCode = deviceCode,
                RecievedWeightId = recievedWeight?.Id ?? null
            };
            await _uw.RecievedMissionRepository.InsertAsync(mission);
        }

        await _uw.CommitAsync();

        return mission.Id;
    }

    public async Task<long> CreateRecievedNumberAsync(long deviceCode, decimal lat, decimal lng, string mobileNumber, long rowNumber, DateTime sendDate)
    {
        var auto = await _uw.AutoRepository.FirstOrDefaultAsync(p => p.DeviceCode == deviceCode);

        var entity = new RecievedNumber
        {
            AutoId = auto?.Id ?? null,
            Lat = lat,
            Lng = lng,
            RowNumber = rowNumber,
            SendDate = sendDate,
            DeviceCode = deviceCode,
            MobileNumber = mobileNumber
        };

        await _uw.RecievedNumberRepository.InsertAsync(entity);
        await _uw.CommitAsync();

        return entity.Id;
    }
}
