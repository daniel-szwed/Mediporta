using System.Text.Json;
using Mediporta.Domain;
using Mediporta.Presentation;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Mediporta.IntegrationTests;

public class IntegrationTests
{
    protected readonly HttpClient _client;
    
    public IntegrationTests()
    {
        var factory = new WebApplicationFactory<Program>();
        _client = factory.CreateClient();
    }
    
    
}