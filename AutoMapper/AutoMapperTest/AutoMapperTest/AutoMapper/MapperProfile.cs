using AutoMapper;
using AutoMapperTest.Models;

namespace AutoMapperTest.AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.Id, options => options.Ignore())
            .ForMember(dest => dest.Name, options => options.Ignore());
    }
}
