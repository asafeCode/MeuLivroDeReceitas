using System.Net;
using System.Text.Json;
using CommonTestUtilities.Requests;
using Shouldly;

namespace WebApi.Test.Recipe;

public class RegisterRecipeTest : MyRecipeBookClassFixture
{
    private const string Method = "api/recipe";
    
    protected RegisterRecipeTest(CustomWebApplicationFactory factory) : base(factory) {}
    
    [Fact]
    public async Task Success()
    {
        var request = RequestRecipeJsonBuilder.Build();
        var response = await DoPost(Method, request);
        
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        
        var responseData = await JsonDocument.ParseAsync(responseBody);
        
        var title = responseData.RootElement.GetProperty("title").GetString();
        var id = responseData.RootElement.GetProperty("id").GetString();
        
        title.ShouldNotBeNullOrWhiteSpace(); title.ShouldNotBe(request.Title);
        id.ShouldNotBeNullOrWhiteSpace(); 
    }
}