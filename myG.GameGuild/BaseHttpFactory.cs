using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace myG.GameGuild
{
    public class BaseHttpFactory
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly JsonSerializer jsonSerializer;
        private readonly IHttpClientFactory httpClientFactory;

        public BaseHttpFactory(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            this.httpClientFactory = httpClientFactory;
            this.httpContextAccessor = httpContextAccessor;
            jsonSerializer = new JsonSerializer();
        }

        public HttpClient CreateHttpClient(string clientName)
        {
            var httpClient = httpClientFactory.CreateClient(clientName);

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

        public async Task<HttpResponseMessage> SendAsync(string clientName, HttpMethod method, string requestUrl, HttpContent content)
        {
            var httpClient = CreateHttpClient(clientName);

            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = httpClient.BaseAddress == null ? new Uri(requestUrl) : new Uri(httpClient.BaseAddress, requestUrl),
                Content = content
            };

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return response;
        }

        public async Task<HttpResponseMessage> SendAsync(string clientName, HttpMethod method, string requestUrl, HttpContent content, AuthenticationHeaderValue value)
        {
            var httpClient = CreateHttpClient(clientName);

            httpClient.Timeout = TimeSpan.FromSeconds(10);
            httpClient.DefaultRequestHeaders.Authorization = value;

            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = httpClient.BaseAddress == null ? new Uri(requestUrl) : new Uri(httpClient.BaseAddress, requestUrl),
                Content = content
            };

            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return response;
        }

        public async Task PostAsync<TRequest>(string clientName, string requestUrl, TRequest request)
        {
            var content = new StringContent(request.JsonSerialize(), Encoding.UTF8, "application/json");
            await SendAsync(clientName, HttpMethod.Post, requestUrl, content);
        }

        public async Task PostAsync(string clientName, string requestUrl, HttpContent content = null)
        {
            if (content == null) content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            await SendAsync(clientName, HttpMethod.Post, requestUrl, content);
        }

        public async Task<Dictionary<HttpMessage, TResponse>> PostAsync<TRequest, TResponse>(string clientName, string requestUrl, TRequest request)
        {
            if (request == null)
            {
                return await PostAsync<TResponse>(clientName, requestUrl, new StringContent(string.Empty, Encoding.UTF8, "application/json"));
            }

            return await PostAsync<TResponse>(clientName, requestUrl, new StringContent(request.JsonSerialize(), Encoding.UTF8, "application/json"));
        }

        public async Task<Dictionary<HttpMessage, TResponse>> PostAsync<TResponse>(string clientName, string requestUrl, HttpContent content = null)
        {
            if (content == null) content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await SendAsync(clientName, HttpMethod.Post, requestUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                return new Dictionary<HttpMessage, TResponse>
                {
                    {new HttpMessage {HttpStatusCode = response.StatusCode, Message = error}, default}
                };
            }

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        return new Dictionary<HttpMessage, TResponse>
                        {
                            { new HttpMessage { HttpStatusCode = response.StatusCode }, jsonSerializer.Deserialize<TResponse>(jsonReader) }
                        };
                    }
                }
            }
        }

        public async Task<Dictionary<HttpMessage, TResponse>> BasicHeaderPostAsync<TResponse>(string clientName, string requestUrl, AuthenticationHeaderValue value, HttpContent content = null)
        {
            if (content == null) content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await SendAsync(clientName, HttpMethod.Post, requestUrl, content, value);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                return new Dictionary<HttpMessage, TResponse>
                {
                    {new HttpMessage {HttpStatusCode = response.StatusCode, Message = error}, default}
                };
            }

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        return new Dictionary<HttpMessage, TResponse>
                        {
                            { new HttpMessage { HttpStatusCode = response.StatusCode }, jsonSerializer.Deserialize<TResponse>(jsonReader) }
                        };
                    }
                }
            }
        }

        public async Task<Dictionary<HttpMessage, TResponse>> GetAsync<TResponse>(string clientName, string requestUrl, HttpContent content = null)
        {
            if (content == null) content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await SendAsync(clientName, HttpMethod.Get, requestUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                return new Dictionary<HttpMessage, TResponse>
                {
                    {new HttpMessage {HttpStatusCode = response.StatusCode, Message = error}, default}
                };
            }

            using (var stream = await response.Content.ReadAsStreamAsync())
            {
                using (var streamReader = new StreamReader(stream))
                {
                    using (var jsonReader = new JsonTextReader(streamReader))
                    {
                        return new Dictionary<HttpMessage, TResponse>
                        {
                            { new HttpMessage { HttpStatusCode = response.StatusCode }, jsonSerializer.Deserialize<TResponse>(jsonReader) }
                        };
                    }
                }
            }
        }
    }

    public class HttpMessage
    {
        public string Message { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
