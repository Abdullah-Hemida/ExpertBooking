using Microsoft.Extensions.DependencyInjection;
using ExpertBooking.Application.Interfaces.Dashboard.Administration;
using ExpertBooking.Application.Services.Dashboard.Administration;
using ExpertBooking.Application.Interfaces.Shared;
using ExpertBooking.Application.Services.Shared;
using ExpertBooking.Application.Interfaces.Website;
using ExpertBooking.Application.Services.Website;
using AutoMapper;
using ExpertBooking.Application.Mapping;


namespace ExpertBooking.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            // Administration
            services.AddScoped<IAdminDashboardService,AdminDashboardService>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            // Shared
            services.AddScoped<IAccountUserService, AccountUserService>();
            services.AddScoped<IFileStorageService, FileStorageService>();

            // Website
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            services.AddScoped<ITokenService, TokenService>();

            
            return services;
        }
    }
}
