using HooToDo.Domain.Models.ToDo;
using HooToDo.Domain.Models.User;
using HooToDo.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HooToDo
{
    public class Startup
    {
        public IConfiguration Config { get; }
        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddMemoryCache();

            services.AddAutoMapper(config =>
            {
                config.AddProfile<ToDoMapperProfile>();
                config.AddProfile<UserMapperProfile>();
            });

            services.AddDbContext<ToDoContext>(options =>
            {
                var connectionString = Config.GetConnectionString("HooToDo");
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<UserModel, IdentityRole<Guid>>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 0;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<ToDoContext>()
                .AddDefaultTokenProviders();
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
            }
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "HooToDo");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
    
}
