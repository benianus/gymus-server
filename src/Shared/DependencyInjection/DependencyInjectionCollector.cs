using gymus_server.GymusApp.Auth;
using gymus_server.GymusApp.Memberships;
using gymus_server.GymusApp.Memberships.Repositories;

namespace gymus_server.Shared.DependencyInjection;

public static class DependencyInjectionCollector
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplicationServices()
        {
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IMembershipService, MembershipService>();
            return services;
        }

        public IServiceCollection AddRepositoryServices()
        {
            services.AddSingleton<UserRepository>();
            services.AddSingleton<MembershipRepository>();
            return services;
        }
    }
}