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

            /*PERBEDAAN SCOPED/SINGELETON/ TRANSIENT
             * - FASE ADALAH DIMANA INSTANCE DIBUAT, KONDISI FASE BERUBAH SAAT REFRESH PAGE / LOAD UI / BUKA TAB BARU 
             *   EX : IBARAT FORGERY TOKEN SEKALI LAOD UI(FASE) AKAN TETAP SAMA , MAU PAKAI AJAX MANAPUN / ACTION TYPE BUTTON APAPAUN , NAMUN JIKA FASE BERUBAH MAKA FORGERY TOKEN BERBAAH
             * 
                1. [SCOPED]         TIDAK AKAN MEMBUAT INSTANCE BARU JIKA DALAM SATU FASE, JIKA FASE BERUBAH MAKA AKAN MEMBUAT INSTANCE BARU
                2. [SINGELETION]    TIDAK AKAN MEMBUAT INSTANCE BARU JIKA DALAM SATU FASE, JIKA FASE BERBUAH MAKA TIDAK AKAN MEMBUAT INSTANCE BARU
                3. [TRANSIENT]      MEMBUAT INSTANCE BARU WALAU DALAM SATU FASE, JIKA FASE BERBUAH MAKA AKAN MEMBUAT INSTANCE BARU
            */

            services.AddDbContext<AppsDbContext>(options =>
                    options.UseNpgsql(configuration.GetConnectionString("AppsDbContext")));
             
            services.AddScoped<IUnitOfWork, UnitOfWork>(); 
            
            //ROLE
            services.AddScoped<IGenericService<Mrole>, MRoleService>(); 
            
            //USER
            services.AddScoped<IGenericService<Muser>, MUserService>(); 

            //MENU
            services.AddScoped<IGenericService<MMenu>, MMenuServices>();
            services.AddScoped<IGeneratedMenu, MMenuServices>();
             
            return services;
        }
    }
}
