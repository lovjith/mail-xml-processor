using Microsoft.AspNetCore.Mvc.Testing;

namespace MailXmlProcessor.Api.Tests;

public class ApiSmokeTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiSmokeTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Swagger_Should_Load()
    {
        var response = await _client.GetAsync("/swagger");
        Assert.True(response.IsSuccessStatusCode);
    }
}
