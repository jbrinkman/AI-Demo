using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebResources.Infrastructure.Data;

namespace WebResources.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWebResourceDb(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<WebResourceDbContext>(options =>
                options.UseSqlServer(connectionString));
        }
    }
}