using HooToDo.Domain.Models.ToDo;
using Microsoft.AspNetCore.Identity;

namespace HooToDo.Domain.Models.User
{
    public class UserModel : IdentityUser<Guid>
    {
        public ICollection<ToDoModel> ToDos { get; set; } = default!;
    }
}