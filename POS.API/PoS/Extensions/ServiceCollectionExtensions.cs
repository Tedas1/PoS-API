using PoS.Abstractions.Repositories.EntityRepositories;
using PoS.Data.Repositories;

namespace PoS.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
