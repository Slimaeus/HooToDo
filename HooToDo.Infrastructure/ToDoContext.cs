using HooToDo.Domain.Models.ToDo;
using HooToDo.Domain.Models.User;
using HooToDo.Infrastructure.EntityConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HooToDo.Infrastructure
{
    public class ToDoContext : IdentityDbContext<UserModel, IdentityRole<Guid>, Guid>
    {
        public ToDoContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ToDoModel> ToDos { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration<ToDoModel>(new ToDoEntityTypeConfiguration());
        }
    }
}