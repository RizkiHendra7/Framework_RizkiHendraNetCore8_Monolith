using FrameWorkRHP_Mono.Core.Models.EF;
using FrameWorkRHP_Mono.Infrastructure.Context;
using FrameWorkRHP_Mono.Infrastructure.UOW;
using FrameWorkRHP_Mono.Services.Interfaces;
using FrameWorkRHP_Mono.Services.Interfaces.GenericInterface;
using FrameWorkRHP_Mono.Services.ServicesImplement;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FrameWorkRHP_Mono.Services
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddDependencyInjectionServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppsDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("AppsDbContext")));
             
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGenericService<Mrole>, MRoleService>();
            services.AddScoped<IGenericService<Muser>, MUserService>();
            services.AddScoped<IGenericService<MMenu>, MMenuServices>();
            services.AddScoped<IGeneratedMenu, MMenuServices>();

            return services;
        }
    }
}
