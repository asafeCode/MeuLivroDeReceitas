using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CommonTestUtilities.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using Shouldly;

namespace WebApi.Test.User.Register;

public class RegisterUserTest(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _httpClient = factory.CreateClient();

    [Fact]
    public async Task Success()
    {
        var request = RequestUserRegisterJsonBuilder.Build();

        var response = await _httpClient.PostAsJsonAsync("User", request);
        
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        
        //responseBody com o conteúdo da response e ler com streamAsync 
        
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        
        //responseBody em um jsonDocument, parse async:
        var responseData = await JsonDocument.ParseAsync(responseBody);
        
        //Json sempre vem em camelCase
        var name = responseData.RootElement.GetProperty("name").GetString();
        
        name.ShouldSatisfyAllConditions(() =>
        {
            name.ShouldNotBeNullOrWhiteSpace();
            name.ShouldBe(request.Name);
        });
    }
}