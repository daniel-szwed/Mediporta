using System.Text.Json;
using Mediporta.Domain;

namespace Mediporta.IntegrationTests;

public class TagControllerTests : IntegrationTests
{
    [Fact]
    public async Task GetTags_ForGivenPageAndSize_ShouldReturnsTags()
    {
        // Arrange
        var page = "1";
        var pageSize = "10";
        var sort = "percentageShare";
        var order = "desc";
        var jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        
        // Act
        var response = await _client
            .GetAsync($"tag?page={page}&pagesize={pageSize}&sort={sort}&order={order}");
        var stringContent = await response.Content.ReadAsStringAsync();
        var tags = JsonSerializer.Deserialize<IEnumerable<Tag>>(stringContent, jsonSerializerOptions);
        
        // Assert
        Assert.True(tags.Count() == 10);
    }

    [Fact]
    public async Task FetchTags_WhenCalled_ShouldRecreateDatabase()
    {
        // Arrange
        var page = "1";
        var pageSize = "2000";
        var sort = "percentageShare";
        var order = "desc";
        var jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        
        // Act
        await _client.PostAsync("tag", new StringContent(string.Empty));
        
        var response = await _client
            .GetAsync($"tag?page={page}&pagesize={pageSize}&sort={sort}&order={order}");
        var stringContent = await response.Content.ReadAsStringAsync();
        var tags = JsonSerializer.Deserialize<IEnumerable<Tag>>(stringContent, jsonSerializerOptions);
        
        // Assert
        Assert.True(tags.Count() == 1000);
    }
}