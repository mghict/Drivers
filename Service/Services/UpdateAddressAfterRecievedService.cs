using AutoMapper;
using Driver.Domain.Entities;
using Driver.Service.IRepositories;
using Microsoft.Extensions.Configuration;
using Moneyon.Common.IOC;
using System.Linq;
using System.Net.Http.Json;

namespace Driver.Service.Services;

[AutoRegister()]
public class UpdateAddressAfterRecievedService
{
    private readonly IUnitOfWork _uw;
    private readonly IMapper _mp;
    private readonly IConfiguration _configuration;

    public UpdateAddressAfterRecievedService(IUnitOfWork uw, IMapper mp, IConfiguration configuration)
    {
        _uw = uw;
        _mp = mp;
        _configuration = configuration;
    }

    public async Task UpdateAddressInStartMission(long entityId=0)
    {
        var mapApi = _configuration!.GetSection("MapApi")!;

        var apiURL = mapApi!.GetSection("ReverseApiAddress").Value;
        var apiKey = mapApi!.GetSection("Key").Value;

        var runService = mapApi!.GetSection("IsReverseAddressServiceEnable").Value;
        if (runService != "1")
            return;

        var runOne = mapApi!.GetSection("IsReverseAddressServiceEnableForPerv").Value;
        IEnumerable<RecievedWeight> recieved_StartedMissionLst ;

        if (runOne == "1")
        {
            recieved_StartedMissionLst = await _uw.RecievedWeightRepository
                                                      .ReadAsync(filter: p => p.IsValidAddress == false &&
                                                                              p.Lat != 0 &&
                                                                              p.Lng != 0);
        }
        else
        {
            var entity = await _uw.RecievedWeightRepository
                                 .FirstOrDefaultAsync(p => p.Id == entityId &&
                                                         p.Lat != 0 &&
                                                         p.Lng != 0);
            if (entity is null)
                return;

            recieved_StartedMissionLst = new List<RecievedWeight> { entity };
        }


        foreach (var item in recieved_StartedMissionLst?.ToList()!)
        {
            long id = await CallServiceAndSaveInDatabaseAsync(item.Lat, item.Lng, apiURL, apiKey);
            if (id == 0)
                continue;

            item.MapReverseId = id;
            item.IsValidAddress = true;
        }

        await _uw.CommitAsync();

    }
    public async Task UpdateAddressInFinishedMission(long entityId = 0)
    {
        var mapApi = _configuration!.GetSection("MapApi")!;

        var apiURL = mapApi!.GetSection("ReverseApiAddress").Value;
        var apiKey = mapApi!.GetSection("Key").Value;

        var runService = mapApi!.GetSection("IsReverseAddressServiceEnable").Value;
        if (runService != "1")
            return;

        var runOne = mapApi!.GetSection("IsReverseAddressServiceEnableForPerv").Value;
        IEnumerable<RecievedMission> recieved_FinishedMissionLst;

        if (runOne == "1")
        {
            recieved_FinishedMissionLst = await _uw.RecievedMissionRepository
                                                   .ReadAsync(filter: p => p.IsValidAddress == false &&
                                                                           p.Lat != 0 &&
                                                                           p.Lng != 0);
        }
        else
        {
            var entity = await _uw.RecievedMissionRepository
                                  .FirstOrDefaultAsync(p => p.Id == entityId &&
                                                            p.Lat != 0 &&
                                                            p.Lng != 0);
            if (entity is null)
                return;

            recieved_FinishedMissionLst = new List<RecievedMission> { entity };
        }

        foreach (var item in recieved_FinishedMissionLst?.ToList()!)
        {
            long id = await CallServiceAndSaveInDatabaseAsync(item.Lat, item.Lng, apiURL, apiKey);
            if (id == 0)
                continue;

            item.MapReverseId = id;
            item.IsValidAddress = true;
        }

        await _uw.CommitAsync();

    }
    public async Task UpdateAddressInSpeedMission(long entityId = 0)
    {
        var mapApi = _configuration!.GetSection("MapApi")!;

        var apiURL = mapApi!.GetSection("ReverseApiAddress").Value;
        var apiKey = mapApi!.GetSection("Key").Value;

        var runService = mapApi!.GetSection("IsReverseAddressServiceEnable").Value;
        if (runService != "1")
            return;

        var runOne = mapApi!.GetSection("IsReverseAddressServiceEnableForPerv").Value;
        IEnumerable<RecievedSpeedAndTemprature> recieved_SpeedLst;

        if (runOne == "1")
        {
            recieved_SpeedLst = await _uw.RecievedSpeedAndTempratureRepository
                                                   .ReadAsync(filter: p => p.IsValidAddress == false &&
                                                                           p.Lat != 0 &&
                                                                           p.Lng != 0);
        }
        else
        {
            var entity = await _uw.RecievedSpeedAndTempratureRepository
                                  .FirstOrDefaultAsync(p => p.Id == entityId &&
                                                            p.Lat != 0 &&
                                                            p.Lng != 0);
            if (entity is null)
                return;

            recieved_SpeedLst = new List<RecievedSpeedAndTemprature> { entity };
        }


        foreach (var item in recieved_SpeedLst?.ToList()!)
        {
            long id = await CallServiceAndSaveInDatabaseAsync(item.Lat, item.Lng, apiURL, apiKey);
            if (id == 0)
                continue;

            item.MapReverseId = id;
            item.IsValidAddress = true;
        }

        await _uw.CommitAsync();

    }
    public async Task UpdateAddressInNumberMission(long entityId = 0)
    {
        var mapApi = _configuration!.GetSection("MapApi")!;

        var apiURL = mapApi!.GetSection("ReverseApiAddress").Value;
        var apiKey = mapApi!.GetSection("Key").Value;

        var runService = mapApi!.GetSection("IsReverseAddressServiceEnable").Value;
        if (runService != "1")
            return;

        var runOne = mapApi!.GetSection("IsReverseAddressServiceEnableForPerv").Value;
        IEnumerable<RecievedNumber> recieved_NumberLst; //= new List<RecievedNumber>();

        if (runOne == "1")
        {
            recieved_NumberLst = await _uw.RecievedNumberRepository
                                                   .ReadAsync(filter: p => p.IsValidAddress == false &&
                                                                           p.Lat != 0 &&
                                                                           p.Lng != 0);
        }
        else
        {
            var entity = await _uw.RecievedNumberRepository
                                  .FirstOrDefaultAsync(p => p.Id == entityId &&
                                                            p.Lat != 0 &&
                                                            p.Lng != 0);
            if (entity is null)
                return;

            recieved_NumberLst=new List<RecievedNumber> { entity };
        }


        foreach (var item in recieved_NumberLst?.ToList()!)
        {
            long id = await CallServiceAndSaveInDatabaseAsync(item.Lat, item.Lng, apiURL, apiKey);
            if (id == 0)
                continue;

            item.MapReverseId = id;
            item.IsValidAddress = true;
        }

        await _uw.CommitAsync();

    }

    private async Task<long> CallServiceAndSaveInDatabaseAsync(decimal lat, decimal lng, string apiURL, string apiKey)
    {
        var addressEntity = await _uw.MapReverseRepository.FirstOrDefaultAsync(p => p.Lat == lat && p.Lng == lng);
        if (addressEntity is not null)
            return addressEntity.Id;

        var addressDto = await CallApiGetAsync<MapReverseDto>(lat, lng, apiURL, apiKey);
        if (addressDto is null || string.IsNullOrWhiteSpace(addressDto.Address))
            return 0;

        var address = _mp.Map<MapReverse>(addressDto);
        await _uw.MapReverseRepository.InsertAsync(address!);
        await _uw.CommitAsync();

        return address.Id;
    }
    private async Task<T?> CallApiGetAsync<T>(decimal lat, decimal lng, string apiUrl, string token)
    {

        using (var client = new HttpClient())
        {

            var requesrtUrl= new Uri($"{apiUrl}?lat={lat}&lon={lng}");

            //client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("x-api-key", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            var response = await client.GetAsync(requesrtUrl);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<T>();
                return result;
            }


        }

        return default(T);
    }
}