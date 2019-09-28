using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Carneiro.Http
{
    internal static class HttpOrchestratorExtensions
    {
        internal static async Task<T> GetContentAsync<T>(this HttpResponseMessage httpResponseMessage) => JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync());
    }
}