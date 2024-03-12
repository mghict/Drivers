using Driver.Common;
using Driver.Common.Models;
using Driver.Domain.Entities;

[ObjectMapper]
public class MaterialMappingProfile : AutoMapper.Profile
{
    public MaterialMappingProfile()
    {
        CreateMap<Material, MaterialModel>();
        CreateMap<MaterialModel,Material>()
            .ForMember(des=>des.Mines,src=>src.Ignore());
    }
}