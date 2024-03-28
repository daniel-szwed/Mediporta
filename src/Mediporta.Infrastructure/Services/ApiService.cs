using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Web;
using Mediporta.Domain.Services;

namespace Mediporta.Infrastructure.Services;

public class ApiService : IApiService, IDisposable
{
    private readonly HttpClient _client;

    public ApiService()
    {
        var clientHandler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate }; 
        _client = new HttpClient(clientHandler);
    }
    
    public async Task<TModel> GetAsync<TModel>(string url, Dictionary<string, string> queryParams)
    {
        var request = new RequestBuilder()
            .SetMethod(HttpMethod.Get)
            .AddHeader("Accept", "application/json")
            .AddHeader("Accept-Encoding", "gzip, deflate")
            .SetUri(url)
            .AddQueryParameters(queryParams)
            .Build();

        var response = await _client.SendAsync(request);
        var stringResponse = await response.Content.ReadAsStringAsync();
        
        return JsonSerializer.Deserialize<TModel>(stringResponse);
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}

public class RequestBuilder
{
    private readonly HttpRequestMessage _requestMessage;

    public RequestBuilder()
    {
        _requestMessage = new HttpRequestMessage();
    }

    public RequestBuilder AddHeader(string header, string value)
    {
        _requestMessage.Headers.Add(header, value);

        return this;
    }
    
    public HttpRequestMessage Build()
    {
        return _requestMessage;
    }

    public RequestBuilder SetMethod(HttpMethod method)
    {
        _requestMessage.Method = method;
        return this;
    }

    public RequestBuilder SetUri(string uri)
    {
        _requestMessage.RequestUri = new Uri(uri);
        return this;
    }
    
    public RequestBuilder AddQueryParameters(Dictionary<string, string> parameters)
    {
        var sb = new StringBuilder();
        foreach (var kvp in parameters)
        {
            if (sb.Length > 0) { sb.Append("&"); }
            sb.AppendFormat("{0}={1}",
                HttpUtility.UrlEncode(kvp.Key),
                HttpUtility.UrlEncode(kvp.Value));
        }

        _requestMessage.RequestUri = new Uri($"{_requestMessage.RequestUri}?{sb}");
        return this;
    }
}