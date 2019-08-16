﻿using Newtonsoft.Json;
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
        /// <param name="httpClientFactory">The HTTP client factory.</param>
        /// <exception cref="ArgumentNullException">options</exception>
        /// <exception cref="InvalidOperationException">Missing a valid url for the client endpoint.</exception>
        public HttpOrchestrator(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(nameof(HttpOrchestrator));
        }

        /// <summary>
        /// Does an Http Get request asynchronously.
        /// </summary>
        /// <param name="uri">The handler uri for the http request.</param>
        /// <returns>An <see cref="HttpResponseMessage"/> with the response.</returns>
        public Task<HttpResponseMessage> GetAsync(string uri) => _httpClient.GetAsync(uri);

        /// <summary>
        /// Does an Http Get request asynchronously.
        /// </summary>
        /// <typeparam name="T">The input type.</typeparam>
        /// <param name="uri">The handler uri for the http request.</param>
        /// <returns>The type defined in <typeparamref name="T"/>.</returns>
        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return await response.GetContentAsync<T>();
        }

        /// <summary>
        /// Does an Http Post request asynchronously.
        /// </summary>
        /// <typeparam name="T">The input type.</typeparam>
        /// <param name="uri">The handler uri for the http request.</param>
        /// <param name="model">The model that goes on the body.</param>
        /// <returns>An <see cref="HttpResponseMessage"/> with the response.</returns>
        public Task<HttpResponseMessage> PostAsync<T>(string uri, T model) => _httpClient.PostAsync(uri, ToStringContent<T>(model));

        /// <summary>
        /// Does an Http Post request asynchronously.
        /// </summary>
        /// <typeparam name="T">The input type.</typeparam>
        /// <param name="uri">The handler uri for the http request.</param>
        /// <param name="model"></param>
        /// <returns>The type defined in <typeparamref name="T"/>.</returns>
        public async Task<T> PostAsync<T>(string uri, object model) where T : class
        {
            HttpResponseMessage response = await _httpClient.PostAsync(uri, ToStringContent(model));
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Does an Http Put request asynchronously.
        /// </summary>
        /// <typeparam name="T">The input type.</typeparam>
        /// <param name="uri">The handler uri for the http request.</param>
        /// <param name="model"></param>
        /// <returns>An <see cref="HttpResponseMessage"/> with the response.</returns>
        public Task<HttpResponseMessage> PutAsync<T>(string uri, T model) => _httpClient.PutAsync(uri, ToStringContent(model));

        /// <summary>
        /// Does an Http Put request asynchronously.
        /// </summary>
        /// <typeparam name="T">The input type.</typeparam>
        /// <param name="uri">The handler uri for the http request.</param>
        /// <param name="model"></param>
        /// <returns>The type defined in <typeparamref name="T"/>.</returns>
        public async Task<T> PutAsync<T>(string uri, object model) where T : class
        {
            HttpResponseMessage response = await _httpClient.PutAsync(uri, ToStringContent(model));
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Does an Http Delete request asynchronously.
        /// </summary>
        /// <param name="uri">The handler uri for the http request.</param>
        /// <returns>An <see cref="HttpResponseMessage"/> with the response.</returns>
        public Task<HttpResponseMessage> DeleteAsync(string uri) => _httpClient.DeleteAsync(uri);

        private StringContent ToStringContent<T>(T model) => new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
    }
}