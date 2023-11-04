using GatewayAPI.Extentions.Extentions;
using GatewayAPI.Extentions.Models;
using GatewayAPI.Extentions.Models.Exceptions;

namespace System.Net.Http
{
    public static class HttpMethodsExtention
    {
        public static async Task<TResult> GetAsync<TResult>(this HttpClient client, string route)
        {
            var response = await client.GetAsync(route);

            if (!response.StatusCode.IsSuccessStatusCode())
            {
                var exception = await SetError(response);
                throw exception;
            }

            return await ReadFromJson<TResult>(response);
        }

        public static async Task<TResult> GetByIdAsync<TResult>(this HttpClient client, string route)
        {
            var response = await client.GetAsync(route);

            if (!response.StatusCode.IsSuccessStatusCode())
            {
                var exception = await SetError(response);
                throw exception;
            }

            return await ReadFromJson<TResult>(response);
        }

        public static async Task<TResult> GetAsJsonAsync<T, TResult>(this HttpClient client, string route, T query)
        {
            var response = await client.GetAsJsonAsync<T>(route, query);

            if (!response.StatusCode.IsSuccessStatusCode())
            {
                var exception = await SetError(response);
                throw exception;
            }

            return await ReadFromJson<TResult>(response);
        }

        public static async Task<TResult> PostAsJsonAsync<T, TResult>(this HttpClient client, string route, T query)
        {
            var response = await client.PostAsJsonAsync<T>(route, query);

            if (!response.StatusCode.IsSuccessStatusCode())
            {
                var exception = await SetError(response);
                throw exception;
            }

            return await ReadFromJson<TResult>(response);
        }

        public static async Task<TResult> PutAsJsonAsync<T, TResult>(this HttpClient client, string route, T query)
        {
            var response = await client.PutAsJsonAsync<T>(route, query);

            if (!response.StatusCode.IsSuccessStatusCode())
            {
                var exception = await SetError(response);
                throw exception;
            }

            return await ReadFromJson<TResult>(response);
        }

        public static async Task<TResult> DeleteAsJsonAsync<T, TResult, TKey>(this HttpClient client, string route, TKey id)
        {
            var response = await client.DeleteAsJsonAsync<TKey>(route, id);

            if (!response.StatusCode.IsSuccessStatusCode())
            {
                var exception = await SetError(response);
                throw exception;
            }

            return await ReadFromJson<TResult>(response);
        }

        private static async Task<TResult> ReadFromJson<TResult>(HttpResponseMessage responseMessage)
        {
            return await responseMessage.Content.ReadAsAsync<TResult>();
        }

        private static async Task<Exception> SetError(HttpResponseMessage responseMessage)
        {
            Exception exception;
            ErrorModel errorMessage = await ReadFromJson<ErrorModel>(responseMessage);

            switch ((int)responseMessage.StatusCode)
            {
                case 400:
                    exception = new BadRequestException(errorMessage.Error);
                    break;
                case 401:
                    exception = new UnauthorizedAccessException(errorMessage.Error);
                    break;
                case 404:
                    exception = new NotFoundException(errorMessage.Error);
                    break;
                case 501:
                    exception = new NotImplementedException(errorMessage.Error);
                    break;
                default:
                    exception = new Exception(errorMessage.Error);
                    break;
            }

            return exception;
        }
    }
}