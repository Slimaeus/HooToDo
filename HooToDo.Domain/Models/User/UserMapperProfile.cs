using AutoMapper;

namespace HooToDo.Domain.Models.User
{
    public class UserMapperProfile : Profile
    {
        public UserMapperProfile()
        {
            CreateMap<RegisterRequest, UserModel>();
        }
    }
}