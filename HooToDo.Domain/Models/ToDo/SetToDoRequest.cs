namespace HooToDo.Domain.Models.ToDo
{
    public class SetToDoRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}