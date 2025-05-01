using Microsoft.Extensions.DependencyInjection;
using ExpertBooking.Application.Interfaces.Dashboard.AdminDashboard;
using ExpertBooking.Application.Services.Dashboard.AdminDashboard;
using ExpertBooking.Application.Interfaces.Shared;
using ExpertBooking.Application.Services.Shared;
using ExpertBooking.Application.Interfaces.Website;
using ExpertBooking.Application.Services.Website;
using AutoMapper;
using ExpertBooking.Application.Mapping;
using ExpertBooking.Application.Interfaces.Dashboard.ExpertDashboard;
using ExpertBooking.Application.Services.Dashboard.ExpertDashboard;


namespace ExpertBooking.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            // AdminDashboard
            services.AddScoped<IAdminDashboardService,AdminDashboardService>();

            // ExpertDashboard
            services.AddScoped<IExpertDashboardService, ExpertDashboardService>();
            // Shared
            services.AddScoped<IAccountUserService, AccountUserService>();
            services.AddScoped<IFileStorageService, FileStorageService>();

            // Website
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            services.AddScoped<ITokenService, TokenService>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }
    }
}
