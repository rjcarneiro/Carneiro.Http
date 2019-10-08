using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Carneiro.Http
{
    internal static class HttpOrchestratorExtensions
    {
        private static JsonSerializerOptions _jsonOptions => new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        internal static async Task<T> GetContentAsync<T>(this HttpResponseMessage httpResponseMessage) => JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(), _jsonOptions);
    }
}