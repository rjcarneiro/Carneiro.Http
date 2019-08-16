using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Carneiro.Http
{
    /// <summary>
    /// Class that handles http requests.
    /// </summary>
    public class HttpOrchestrator
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpOrchestrator"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <exception cref="ArgumentNullException">options</exception>
        /// <exception cref="InvalidOperationException">Missing a valid url for the client endpoint.</exception>
        public HttpOrchestrator(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(HttpOrchestrator));
        }

        /// <summary>
        /// Does an Http Get asynchronously.
        /// </summary>
        /// <param name="uri">The handler uri for the http request.</param>
        /// <returns>An <see cref="HttpResponseMessage"/> with the response.</returns>
        public Task<HttpResponseMessage> GetAsync(string uri) => _httpClient.GetAsync(uri);

        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return await response.GetContentAsync<T>();
        }

        /// <summary>
        /// Does an Http Post asynchronously.
        /// </summary>
        /// <typeparam name="T">The input type.</typeparam>
        /// <param name="uri">The handler uri for the http request.</param>
        /// <param name="model">The body to go on the body.</param>
        /// <returns>An <see cref="HttpResponseMessage"/> with the response.</returns>
        public Task<HttpResponseMessage> PostAsync<T>(string uri, T model) => _httpClient.PostAsync(uri, ToStringContent<T>(model));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<T> PostAsync<T>(string uri, object model) where T : class
        {
            HttpResponseMessage response = await _httpClient.PostAsync(uri, ToStringContent(model));
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> PutAsync<T>(string uri, T model) => _httpClient.PutAsync(uri, ToStringContent(model));

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<T> PutAsync<T>(string uri, object model) where T : class
        {
            HttpResponseMessage response = await _httpClient.PutAsync(uri, ToStringContent(model));
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public Task<HttpResponseMessage> DeleteAsync(string uri) => _httpClient.DeleteAsync(uri);

        private StringContent ToStringContent<T>(T model) => new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
    }
}