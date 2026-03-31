using gymus_server.GymusApp.Employees;
using gymus_server.GymusApp.Owners;
using gymus_server.GymusApp.Persons.Models;
using gymus_server.GymusApp.Persons.Repository;
using gymus_server.GymusApp.Persons.Service;
using gymus_server.GymusApp.Users;
using gymus_server.GymusApp.Users.Repository;
using gymus_server.GymusApp.Users.Service;
using gymus_server.Shared.Abstractions;

namespace gymus_server.Shared.DependencyInjection;

public static class DependencyInjectionCollector
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddApplicationServices()
        {
            services.AddScoped<ICrudService<Person, int>, PersonService>();
            services.AddScoped<ICrudService<User, int>, UserService>();
            services.AddScoped<ICrudService<Owner, int>, OwnerService>();
            services.AddScoped<ICrudService<Employee, int>, EmployeeService>();
            return services;
        }

        public IServiceCollection AddRepositoryServices()
        {
            services.AddScoped<ICrudRepository<Person, int>, PersonRepository>();
            services.AddScoped<ICrudRepository<User, int>, UserRepository>();
            services.AddScoped<ICrudRepository<Owner, int>, OwnerRepository>();
            services.AddScoped<ICrudRepository<Employee, int>, EmployeeRepository>();
            return services;
        }
    }
}