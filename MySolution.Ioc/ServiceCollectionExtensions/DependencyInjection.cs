using Microsoft.Extensions.DependencyInjection;
using eCommerce.Domain.Interfaces.Repository;
using eCommerce.Domain.Interfaces.Services;
using eCommerce.Infrastructure.Repository;
using eCommerce.Services;

namespace eCommerce.Ioc.ServiceCollectionExtensions
{
    public static class DependencyInjection
    {
        #region Methods
        public static void ConfigureDependencyInjectioneCommerce(this IServiceCollection services)
        {
            // Services
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IProductServices, ProductServices>();

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
        #endregion
    }
}