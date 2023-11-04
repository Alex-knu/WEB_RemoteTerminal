using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace System.Net.Http
{
    public static class HttpClientJsonExtensions
    {
        public static async Task<HttpResponseMessage> DeleteAsJsonAsync<TValue>(this HttpClient client, string requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(value, options);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var request = new HttpRequestMessage(HttpMethod.Delete, requestUri)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            return await client.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
        }

        public static async Task<HttpResponseMessage> GetAsJsonAsync<TValue>(this HttpClient client, string requestUri, TValue value, JsonSerializerOptions? options = null, CancellationToken cancellationToken = default)
        {
            var json = JsonSerializer.Serialize(value, options);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using var request = new HttpRequestMessage(HttpMethod.Get, requestUri)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };

            return await client.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false);
        }
    }
}