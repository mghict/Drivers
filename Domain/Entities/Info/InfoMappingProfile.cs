using Common.Extensions;
using Driver.Common;
using Driver.Common.Models;
using Driver.Domain.Entities;

namespace Domain.Entities;

[ObjectMapper]
public class InfoMappingProfile : AutoMapper.Profile
{
    public InfoMappingProfile()
    {
        CreateMap<Location, LocationModel>()
            .ForMember(des => des.CityName, src => src.MapFrom(src => src.City.Name))
            .ForMember(des => des.ProvinceId, src => src.MapFrom(src => src.City.ProvinceId))
            .ForMember(des => des.ProvinceName, src => src.MapFrom(src => src.City.Province.Name));

        CreateMap<LocationCreateModel, Location>();

        CreateMap<MapReverseDto, MapReverse>()
            .ForMember(des => des.Lat, src => src.MapFrom(src =>src.Geom!.Coordinates[1]!.ToString().ToDecimal()))
            .ForMember(des => des.Lng, src => src.MapFrom(src => src.Geom!.Coordinates[0]!.ToString().ToDecimal()));


        CreateMap<City, CityShortModel>().ReverseMap();
        
        CreateMap<City, CityModel>()
            .ForMember(des => des.ProvinceName, src => src.MapFrom(src => src.Province.Name));

        CreateMap<Province, ProvinceShortModel>();

        CreateMap<ProvinceShortModel,Province>()
            .ForMember(des => des.CountryId, src => src.MapFrom(src => 1));
    }
}