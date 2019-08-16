using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Carneiro.Http
{
    internal static class HttpOrchestratorExtensions
    {
        internal static async Task<T> GetContentAsync<T>(this HttpResponseMessage httpResponseMessage) => JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync());
    }
}