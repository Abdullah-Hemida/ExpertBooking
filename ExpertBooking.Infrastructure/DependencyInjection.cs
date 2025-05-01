using Microsoft.Extensions.DependencyInjection;
using ExpertBooking.Core.IRepositories.Dashboard.AdminDashboard;
using ExpertBooking.Infrastructure.Repositories.Dashboard.AdminDashboard;
using ExpertBooking.Core.IRepositories.Shared;
using ExpertBooking.Infrastructure.Repositories.Shared;
using ExpertBooking.Core.IRepositories.Website;
using ExpertBooking.Infrastructure.Repositories.Website;
using ExpertBooking.Core.IRepositories.Dashboard.ExpertDashboard;
using ExpertBooking.Infrastructure.Repositories.Dashboard.ExpertDashboard;

namespace ExpertBooking.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // AdminDashboard
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IExpertRepository, ExpertRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            // ExpertDashboard
            services.AddScoped<IExpertDashboardRepository, ExpertDashboardRepository>();
            // Shared
            services.AddScoped<IExpertDocumentRepository, ExpertDocumentRepository>();
            services.AddScoped<IExpertUserRepository, ExpertUserRepository>();
            services.AddScoped<IClientUserRepository, ClientUserRepository>();
            services.AddScoped<IAccountUserRepository, AccountUserRepository>();

            // Website
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            return services;
        }
    }
}
