using Common.Models;
using Driver.Common;
using Driver.Common.Models;

namespace Domain.Entities;

[ObjectMapper]
public class UserMappingProfile : AutoMapper.Profile
{
    public UserMappingProfile()
    {


        CreateMap<User, UserIdentityModel>()
            .ForMember(des => des.Roles, src => src.MapFrom(src => src.GetRoles().ToArray()))
            .ForMember(des => des.Permissions, src => src.MapFrom(src => src.GetPermission().ToArray()))
            .ForMember(des => des.DisplayName, src => src.MapFrom(src => src.Person!.DisplayName))
            .ForMember(des => des.UserName, src => src.MapFrom(src => src.UserName))
            .ForMember(des => des.PersonCode, src => src.MapFrom(src => src.Person.PersonCode));

        
    }
}
