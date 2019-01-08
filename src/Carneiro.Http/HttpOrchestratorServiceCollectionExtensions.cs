using Microsoft.Extensions.DependencyInjection;

namespace Carneiro.Http
{
    /// <summary>
    /// Class that helps register <see cref="HttpOrchestrator"/> service.
    /// </summary>
    public static class HttpOrchestratorServiceCollectionExtensions
    {
        public static IServiceCollection AddHttpOrchestrator(this IServiceCollection services)
        {
            services.AddTransient<HttpOrchestrator>();
            return services;
        }

        public static IServiceCollection AddHttpOrchestrator(this IServiceCollection services, string url)
        {
            services = services.AddHttpOrchestrator();

            services.AddOptions();
            services.Configure<HttpOrchestratorOptions>(t => t.Url = url);

            return services;
        }
    }
}
