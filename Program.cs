using Lab1.Entity;
using Lab1.Service;
using Microsoft.EntityFrameworkCore;

namespace Lab1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigureServices(builder.Services, builder.Configuration);
             builder.Services.AddCors(
                            option => {
                                option.AddPolicy("AddPolicy", crosPolicy =>
                                {
                                    crosPolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                                });
                            }
                        );
            var app = builder.Build();
           
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapControllers();
            app.UseCors("AddPolicy");
            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<UnivDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Con"));
            });

            
            services.AddTransient<IStudentRepo, StudentRepo>();
            services.AddTransient<IDepartmentRepo, DepartmentRepo>();
            services.AddTransient<IManagerRepo, ManagerRepo>();
        }
    }
}
