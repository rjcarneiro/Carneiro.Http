﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;

namespace Carneiro.Http
{
    /// <summary>
    /// Options for <see cref="HttpOrchestrator"/>.
    /// </summary>
    public class HttpOrchestratorOptions
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }

        public override string ToString() => Url;
    }

    internal static class HttpOrchestratorExtensions
    {
        internal static async Task<T> GetContentAsync<T>(this HttpResponseMessage httpResponseMessage)
        {
            return JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync());
        }
    }

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
        /// <exception cref="ArgumentNullException">Case <paramref name="options"/> is null.</exception>
        /// <exception cref="InvalidOperationException">Case <seealso cref="HttpOrchestratorOptions.Url"/> is null, empty or have an invalid uri schema.</exception>
        public HttpOrchestrator(IOptions<HttpOrchestratorOptions> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            HttpOrchestratorOptions orchestratorOptions = options.Value;

            if (string.IsNullOrEmpty(orchestratorOptions.Url) || Uri.IsWellFormedUriString(orchestratorOptions.Url, UriKind.Absolute))
                throw new InvalidOperationException("Missing a valid url for the client endpoint.");

            _httpClient = HttpClientFactory.Create();
            _httpClient.BaseAddress = new Uri(orchestratorOptions.Url);
        }

        public Task<HttpResponseMessage> GetAsync(string uri) => _httpClient.GetAsync(uri);

        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return await response.GetContentAsync<T>();
        }

        public Task<HttpResponseMessage> PostAsync<T>(string uri, T model)
        {
            return _httpClient.PostAsync<T>(uri, model, new JsonMediaTypeFormatter());
        }

        public async Task<T> PostAsync<T>(string uri, object model) where T : class
        {
            HttpResponseMessage response = await _httpClient.PostAsync(uri, model, new JsonMediaTypeFormatter());
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public Task<HttpResponseMessage> PutAsync<T>(string uri, T model)
        {
            return _httpClient.PutAsync<T>(uri, model, new JsonMediaTypeFormatter());
        }

        public async Task<T> PutAsync<T>(string uri, object model) where T : class
        {
            HttpResponseMessage response = await _httpClient.PutAsync(uri, model, new JsonMediaTypeFormatter());
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
        }

        public Task<HttpResponseMessage> DeleteAsync(string uri) => _httpClient.DeleteAsync(uri);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
