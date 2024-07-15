namespace ProductStore.Server.Middlewares
{
    public static class ServiceExtensions
    {
        public static void AddMiddlewares(this IServiceCollection services)
        {
            services.AddSingleton<ExceptionHandlingMiddleware>();
        }
    }
}
