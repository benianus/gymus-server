using gymus_server.GymusApp.Auth;
using gymus_server.GymusApp.Memberships;
using gymus_server.GymusApp.Memberships.Repositories;
using gymus_server.GymusApp.Reports;
using gymus_server.GymusApp.Sessions;
using gymus_server.GymusApp.Sessions.Repositories;
using gymus_server.GymusApp.Store;
using gymus_server.Shared.Infrastructures;

namespace gymus_server.Shared.DependencyInjection;

public static class DependencyInjectionCollector {
    extension(IServiceCollection services) {
        public IServiceCollection AddApplicationServices() {
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IMembershipService, MembershipService>();
            services.AddSingleton<ISessionService, SessionService>();
            services.AddSingleton<ISalesReportsService, ReportsService>();
            services.AddSingleton<IStoreService, StoreService>();
            services.AddSingleton<IRevenueReportsService, ReportsService>();
            services.AddSingleton<ISalesReportsService, ReportsService>();
            services.AddSingleton<IDbConnectionFactory, NpgSqlConnectionFactory>();
            return services;
        }

        public IServiceCollection AddRepositoryServices() {
            services.AddSingleton<UserRepository>();
            services.AddSingleton<MembershipRepository>();
            services.AddSingleton<SessionRepository>();
            services.AddSingleton<ReportsRepository>();
            services.AddSingleton<StoreRepository>();
            return services;
        }
    }
}