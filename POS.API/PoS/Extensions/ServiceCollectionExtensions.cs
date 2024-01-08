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
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IItemRepository, ItemRepository>();
            services.AddScoped<IItemOrderRepository, ItemOrderRepository>();
            services.AddScoped<ITipRepository, TipRepository>();
            services.AddScoped<ITaxRepository, TaxRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IReservationRepository, ReservationRepository>();
            services.AddScoped<ILoyaltyProgramRepository, LoyaltyProgramRepository>();

            return services;
        }
    }
}
