using HooToDo.Domain.Models.User;
using Microsoft.AspNetCore.Identity;

namespace HooToDo.Domain.Models.ToDo
{
    public class ToDoModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public Guid UserId { get; set; }
        public UserModel User { get; set; } = default!;
    }
}