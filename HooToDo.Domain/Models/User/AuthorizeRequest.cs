namespace HooToDo.Domain.Models.User
{
    public class AuthorizeRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool RememberMe { get; set; } = false;
    }
}