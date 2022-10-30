using AutoMapper;

namespace HooToDo.Domain.Models.ToDo
{
    public class ToDoMapperProfile : Profile
    {
        public ToDoMapperProfile()
        {
            CreateMap<SetToDoRequest, ToDoModel>();
            CreateMap<ToDoModel, ToDoResponse>();
        }
    }
}