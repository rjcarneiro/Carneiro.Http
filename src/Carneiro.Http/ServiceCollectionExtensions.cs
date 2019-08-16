using Microsoft.Extensions.DependencyInjection;
using System;

namespace Carneiro.Http
{
    /// <summary>
    /// Class that helps register <see cref="HttpOrchestrator"/> service.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers the <see cref="HttpOrchestrator"/> and a new http client named "HttpOrchestrator".
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="url">The URL.</param>
        /// <returns></returns>
        public static IServiceCollection RegisterHttpOrchestrator(this IServiceCollection services, string url)
        {
            if (string.IsNullOrEmpty(url) || !Uri.IsWellFormedUriString(url, UriKind.Absolute))
                throw new InvalidOperationException("Missing a valid url for the client endpoint.");
            
            services.AddTransient<HttpOrchestrator>();
            services.AddHttpClient(nameof(HttpOrchestrator), c =>
            {
                c.BaseAddress = new Uri(url);
            });

            return services;
        }
    }
}
