using Driver.Common;
using Driver.Common.Models;

namespace Domain.Entities;

[ObjectMapper]
public class PersonMappingProfile : AutoMapper.Profile
{
    public PersonMappingProfile()
    {
        CreateMap<Person, PersonModel>();

        CreateMap<Person, PersonDetailDto>();

        CreateMap<Person, PersonShortModel>();

        CreateMap<PersonCreateModel, Person>()
            .ForMember(des=>des.PersonCode,src=> src.MapFrom(src => Guid.NewGuid()))
            .ForMember(des => des.IsActive, src => src.MapFrom(src=> true));

        CreateMap<PersonUpdateModel, Person>();

        CreateMap<Person, UserModel>()
            .ForMember(des => des.UserName, src => src.MapFrom(src => src.User!.UserName))
            .ForMember(des => des.Role, src => src.MapFrom(src => src.User!.Role!))
            .ForMember(des => des.Provinces, src => src.MapFrom(src => src.User!.Provinces!))
            .ForMember(des => des.Mines, src => src.MapFrom(src => src.User!.Mines!));

        CreateMap<Role, RoleModel>()
            .ForMember(des => des.Name, src => src.MapFrom(src => src.RoleName)); ;

        CreateMap<UserCreateModel, Person>()
            .ForMember(des => des.PersonCode, src => src.MapFrom(src => Guid.NewGuid()))
            .ForMember(des => des.IsActive, src => src.MapFrom(src => true))
            .ForMember(des => des.Type, src => src.MapFrom(src => PersonType.Users));

        CreateMap<UserUpdateModel, Person>();
            //.ForMember(des => des.User.RoleId, src => src.MapFrom(src => src.Role.Id))
            //.ForMember(des => des.User.IsActive, src => src.MapFrom(src => src.IsActive));
    }
}


