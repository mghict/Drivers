using Driver.Common.Models;
using Driver.Common;
using Driver.Domain.Entities;
using Common.Extensions;
using Moneyon.Common.Extensions;

namespace Domain.Entities;

[ObjectMapper]
public class AutoMappingProfile : AutoMapper.Profile
{
    public AutoMappingProfile()
    {
        CreateMap<AutoBrand, AutoBrandModel>().ReverseMap();
        CreateMap<AutoModel, AutoModelsModel>()
            .ForMember(des=>des.AutoBrandName,src=>src.MapFrom(src=>src.AutoBrand.Name));

        CreateMap<AutoModelsModel, AutoModel>();

        CreateMap<Auto, AutoDto>()
            .ForMember(des => des.PersonCode, src => src.MapFrom(src => src.Person.PersonCode));

        CreateMap<Auto, AutoDetailsDto>()
            .ForMember(des => des.DriverPersonCode, src => src.MapFrom(src => src.Person.PersonCode))
            .ForMember(des => des.DriverFirstName, src => src.MapFrom(src => src.Person.FirstName))
            .ForMember(des => des.DriverLastName, src => src.MapFrom(src => src.Person.LastName))
            .ForMember(des => des.DriverNationalCode, src => src.MapFrom(src => src.Person.NationalCode))
            .ForMember(des => des.DriverMobileNumber, src => src.MapFrom(src => src.Person.MobileNumber))
            .ForMember(des => des.DriverBirthDate, src => src.MapFrom(src => src.Person.BirthDate))
            .ForMember(des => des.DriverBirthDateShamsi, src => src.MapFrom(src => src.Person.BirthDate.ToJalaliDate()))
            .ForMember(des => des.DriverIsActive, src => src.MapFrom(src => src.Person.IsActive))
            .ForMember(des => des.DriverPic, src => src.MapFrom(src => src.Person!.Document!.Content.Value.ToBase64()))
            .ForMember(des => des.AutoPic, src => src.MapFrom(src => src.Document!.Content.Value.ToBase64()))
            .ForMember(des => des.AutoBrandName, src => src.MapFrom(src => src.AutoModel.AutoBrand.Name))
            .ForMember(des => des.AutoModelName, src => src.MapFrom(src => src.AutoModel.Name))
            .ForMember(des => des.MineCode, src => src.MapFrom(src => src.Mine.MineCode))
            .ForMember(des => des.MineName, src => src.MapFrom(src => src.Mine.Name))
            .ForMember(des => des.MineProvinceName, src => src.MapFrom(src => src.Mine.Location.City.Province.Name))
            .ForMember(des => des.MineCityName, src => src.MapFrom(src => src.Mine.Location.City.Name))
            .ForMember(des => des.MineAddress, src => src.MapFrom(src => src.Mine.Address))
            .ForMember(des => des.AutoIsActive, src => src.MapFrom(src => src.IsActive))
            ;

        CreateMap<AutoCreateModel, Auto>()
            .ForMember(des => des.IsActive, src => src.MapFrom(src => true))
            .ForMember(des => des.Mine, src => src.Ignore())
            .ForMember(des => des.MineId, src => src.MapFrom(src => src.Mine.Id));

        CreateMap<AutoUpdateModel, Auto>()
            .ForMember(des => des.Mine, src => src.Ignore())
            .ForMember(des => des.MineId, src => src.MapFrom(src => src.Mine.Id));

        CreateMap<RecievedWeight,AutoMissionsModel>()
            .ForMember(des => des.CityId, src => src.MapFrom(src => src.Auto!.Mine!.Location!.CityId))
            .ForMember(des => des.CityName, src => src.MapFrom(src => src.Auto!.Mine!.Location!.City!.Name))
            .ForMember(des => des.ProvinceId, src => src.MapFrom(src => src.Auto!.Mine!.Location!.City!.ProvinceId))
            .ForMember(des => des.ProvinceName, src => src.MapFrom(src => src.Auto!.Mine!.Location!.City!.Province.Name))
            .ForMember(des => des.MineId, src => src.MapFrom(src => src.Auto!.Mine!.Id))
            .ForMember(des => des.MineName, src => src.MapFrom(src => src.Auto!.Mine!.Name))
            .ForMember(des => des.DriverDisplayName, src => src.MapFrom(src => src.Auto!.Person!.DisplayName))
            .ForMember(des => des.StartDate, src => src.MapFrom(src => src.SendDate))
            .ForMember(des => des.EndDate, src => src.MapFrom(src => src.RecievedMission!.SendDate))
            .ForMember(des => des.Weight, src => src.MapFrom(src => src.Weight))
            .ForMember(des => des.AutoWeight, src => src.MapFrom(src => src.Auto!.Weight))
            .ForMember(des => des.Type, src => src.MapFrom(src => src.Type))
            .ForMember(des => des.AutoId, src => src.MapFrom(src => src.Auto!.Id))
            .ForMember(des => des.Pelak, src => src.MapFrom(src => src.Auto!.Pelak))
            .ForMember(des => des.VIN, src => src.MapFrom(src => src.Auto!.VIN))
            .ForMember(des => des.Id, src => src.MapFrom(src => src.Id))
            .ForMember(des => des.DeviceCode, src => src.MapFrom(src => src.DeviceCode))
            .ForMember(des => des.MaterialId, src => src.MapFrom(src => src.MaterialId))
            .ForMember(des => des.MaterialName, src => src.MapFrom(src => src.Material!.Name))
            ;

        CreateMap<RecievedSpeedAndTemprature, AutoTransportModel>();

        CreateMap<RecievedError, AutoErrorDto>()
            .ForMember(des => des.ErrorCodeMessage, src => src.MapFrom(src => src.ErrorCode!.ErrorMessage));

        CreateMap<RecievedError, AutoOffDeviceDto>()
            .ForMember(des => des.DriverDisplayName, src => src.MapFrom(src => src.Auto!.Person!.DisplayName))
            .ForMember(des => des.OffDate, src => src.MapFrom(src => src.SendDate))
            .ForMember(des => des.MineName, src => src.MapFrom(src => src.Auto!.Mine!.Name))
            .ForMember(des => des.Id, src => src.MapFrom(src => src.AutoId))
            .ForMember(des => des.DeviceCode, src => src.MapFrom(src => src.DeviceCode))
            .ForMember(des => des.Pelak, src => src.MapFrom(src => src.Auto!.Pelak))
            .ForMember(des => des.AutoModelName, src => src.MapFrom(src => src.Auto!.AutoModel!.Name))
            ;
    }
}