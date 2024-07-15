using FluentValidation;
using ProductStore.Server.AppStart;
using ProductStore.Server.Converters;
using ProductStore.Server.Validators;

namespace ProductStore.Server
{
    public static class ServiceExtensions
    {
        public static void AddServerServices(this IServiceCollection services)
        {
            services.AddScoped<IResponseMessagesBuilder, ResponseMessagesBuilder>();
            services.AddValidatorsFromAssemblyContaining<ProductValidator>();
            services.ConfigureOptions<ApiJsonOptions>();
        }
    }
}
