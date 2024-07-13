using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductStore.Application.Interfaces;
using ProductStore.Application.Services;
using ProductStore.Infrastructure;

namespace ProductStore.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddInfrastructureServices(configuration);
            return services;
        }
    }
}
