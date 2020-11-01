using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectManagementApp.BLL.MappingProfiles;
using ProjectManagementApp.BLL.Services;
using ProjectManagementApp.Common.DTO;
using ProjectManagementApp.ProjectManagementApp.Context;
using ProjectManagementApp.ProjectManagementApp.Interfaces;
using ProjectManagementApp.ProjectManagementApp.UnitOfWork;

namespace ProjectManagementApp.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("dbConnection")));

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ProjectProfile());
                mc.AddProfile(new ProjectsInfoProfile());
                mc.AddProfile(new TaskProfile());
                mc.AddProfile(new TaskStateProfile());
                mc.AddProfile(new TeamProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new UsersInfoProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddControllers().AddNewtonsoftJson();

            services.AddControllers();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IService<ProjectDTO>, ProjectService>();
            services.AddScoped<IService<TaskDTO>, TaskService>();
            services.AddScoped<IService<TaskStateDTO>, TaskStateService>();
            services.AddScoped<IService<TeamDTO>, TeamService>();
            services.AddScoped<IService<UserDTO>, UserService>();
            services.AddScoped<LINQService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
