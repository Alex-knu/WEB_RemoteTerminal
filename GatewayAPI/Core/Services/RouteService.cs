using GatewayAPI.Core.Interfaces;

namespace GatewayAPI.Core.Services
{
    public class RouteService : IRouteService
    {
        public async Task<TResult> GetAllAsync<TResult>(HttpClient client, string controller)
        {
            return await client.GetAsync<TResult>($"api/{controller}/collection");
        }

        public async Task<TResult> GetListByIdAsync<TResult, TKey>(HttpClient client, string controller, TKey id)
        {
            return await client.GetByIdAsync<TResult>($"api/{controller}/collection/{id}");
        }

        public async Task<TResult> GetByIdAsync<TResult, TKey>(HttpClient client, string controller, TKey id)
        {
            return await client.GetByIdAsync<TResult>($"api/{controller}/{id}");
        }

        public async Task<TResult> GetAsJsonAsync<T, TResult>(HttpClient client, string controller, T query)
        {
            return await client.GetAsJsonAsync<T, TResult>($"api/{controller}/collection", query);
        }

        public async Task<TResult> PostAsJsonAsync<T, TResult>(HttpClient client, string controller, T query)
        {
            return await client.PostAsJsonAsync<T, TResult>($"api/{controller}", query);
        }

        public async Task<TResult> PutAsJsonAsync<T, TResult>(HttpClient client, string controller, T query)
        {
            return await client.PutAsJsonAsync<T, TResult>($"api/{controller}", query);
        }

        public async Task<TResult> DeleteAsJsonAsync<TResult, TKey>(HttpClient client, string controller, TKey id)
        {
            return await client.DeleteAsJsonAsync<TKey, TResult, TKey>($"api/{controller}", id);
        }
    }
}