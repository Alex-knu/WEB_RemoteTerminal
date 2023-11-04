namespace GatewayAPI.Core.Interfaces
{
    public interface IRouteService
    {
        Task<TResult> DeleteAsJsonAsync<TResult, TKey>(HttpClient client, string controller, TKey id);
        Task<TResult> GetAllAsync<TResult>(HttpClient client, string controller);
        Task<TResult> GetByIdAsync<TResult, TKey>(HttpClient client, string controller, TKey id);
        Task<TResult> GetListByIdAsync<TResult, TKey>(HttpClient client, string controller, TKey id);
        Task<TResult> GetAsJsonAsync<T, TResult>(HttpClient client, string controller, T query);
        Task<TResult> PostAsJsonAsync<T, TResult>(HttpClient client, string controller, T query);
        Task<TResult> PutAsJsonAsync<T, TResult>(HttpClient client, string controller, T query);
    }
}