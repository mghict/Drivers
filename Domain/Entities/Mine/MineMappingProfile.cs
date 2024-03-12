using Driver.Common;
using Driver.Common.Models;

namespace Domain.Entities;

[ObjectMapper]
public class MineMappingProfile : AutoMapper.Profile
{
    public MineMappingProfile()
    {

        CreateMap<Mine, MineModel>()
            .ForMember(des => des.MaterialIds, src => src.MapFrom(src => src.Materials!.Select(p=>p.Id).ToList()));

        CreateMap<Mine, MineShortModel>()
            .ForMember(des => des.CityName, src => src.MapFrom(src => src.Location.City.Name))
            .ForMember(des => des.ProvinceName, src => src.MapFrom(src => src.Location.City.Province.Name));

        CreateMap<MineUpdateModel, Mine>()
            .ForMember(des => des.Materials, src => src.Ignore());
    }
}