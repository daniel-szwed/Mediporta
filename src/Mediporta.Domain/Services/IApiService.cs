namespace Mediporta.Domain.Services;

public interface IApiService
{
    Task<TModel> GetAsync<TModel>(string url, Dictionary<string, string> queryParams);
}