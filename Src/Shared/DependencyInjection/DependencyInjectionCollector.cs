using FluentValidation;
using gymus_server.GymusApp.Auth;
using gymus_server.GymusApp.Auth.Dtos.Requests;
using gymus_server.GymusApp.Auth.Models;
using gymus_server.GymusApp.Memberships;
using gymus_server.GymusApp.Memberships.Dtos.Requests;
using gymus_server.GymusApp.Memberships.Repositories;
using gymus_server.GymusApp.Reports;
using gymus_server.GymusApp.Sessions;
using gymus_server.GymusApp.Sessions.Dtos.Requests;
using gymus_server.GymusApp.Sessions.Repositories;
using gymus_server.GymusApp.Store;
using gymus_server.GymusApp.Store.Dtos.Requests;
using gymus_server.Shared.Infrastructures;
using gymus_server.Shared.Security;
using gymus_server.Shared.Validations.AuthValidations;
using gymus_server.Shared.Validations.MembershipValidations;
using gymus_server.Shared.Validations.SessionValidations;
using gymus_server.Shared.Validations.StoreValidations;
using Microsoft.AspNetCore.Identity;

namespace gymus_server.Shared.DependencyInjection;

public static class DependencyInjectionCollector {
    extension(IServiceCollection services) {
        public IServiceCollection AddApplicationServices() {
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IMembershipService, MembershipService>();
            services.AddSingleton<ISessionService, SessionService>();
            services.AddSingleton<ISalesReportsService, SalesReportsService>();
            services.AddSingleton<IStoreService, StoreService>();
            services.AddSingleton<IRevenueReportsService, RevenueReportsService>();
            services.AddSingleton<ISalesReportsService, SalesReportsService>();
            services.AddSingleton<IDbConnectionFactory, NpgSqlConnectionFactory>();
            services.AddSingleton<PasswordHasher<User>>();
            services.AddSingleton<JwtHelpers>();
            return services;
        }

        public IServiceCollection AddRepositoryServices() {
            services.AddSingleton<UserRepository>();
            services.AddSingleton<MembershipRepository>();
            services.AddSingleton<SessionRepository>();
            services.AddSingleton<StoreRepository>();
            services.AddSingleton<SalesReportsRepository>();
            services.AddSingleton<RevenueReportsRepository>();
            return services;
        }

        public IServiceCollection AddValidatorsServices() {
            services.AddScoped<IValidator<LoginRequestDto>, LoginRequestValidation>();
            services.AddScoped<IValidator<RegisterRequestDto>, RegisterRequestValidation>();
            services
               .AddScoped<IValidator<RegisterMemberRequestDto>, RegisterMemberRequestValidation>();
            services.AddScoped<IValidator<MemberUpdateRequestDto>, UpdateMemberRequestValidation>();
            services
               .AddScoped<IValidator<SessionRegisterRequestDto>,
                    SessionRegisterRequestValidation>();
            services
               .AddScoped<IValidator<ProductCreateRequestDto>, CreateProductRequestValidation>();
            services
               .AddScoped<IValidator<ProductUpdateRequestDto>, UpdateProductRequestValidation>();
            services.AddScoped<IValidator<SaleRegisterRequestDto>, RegisterSaleRequestValidation>();
            return services;
        }
    }
}