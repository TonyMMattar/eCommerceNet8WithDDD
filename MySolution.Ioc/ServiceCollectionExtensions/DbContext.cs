using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using eCommerce.Infrastructure.Context;

namespace eCommerce.Ioc.ServiceCollectionExtensions
{
    public static class DbContext
    {
        #region Methods
        public static void AddDbContextMyDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MyDatabaseContext>(options => options.UseSqlServer(connectionString));
        }
        #endregion
    }
}