using HooToDo.Domain.Models.ToDo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HooToDo.Infrastructure.EntityConfigurations
{
    public class ToDoEntityTypeConfiguration : IEntityTypeConfiguration<ToDoModel>
    {
        public void Configure(EntityTypeBuilder<ToDoModel> builder)
        {
            
        }
    }
}