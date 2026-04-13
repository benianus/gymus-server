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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMembershipService, MembershipService>();
            return services;
        }

        public IServiceCollection AddRepositoryServices()
        {
            services.AddScoped<UserRepository>();
            services.AddScoped<MembershipRepository>();
            return services;
        }
    }
}