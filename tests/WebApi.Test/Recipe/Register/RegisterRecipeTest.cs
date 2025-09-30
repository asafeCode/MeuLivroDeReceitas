using System.Net;
using System.Text.Json;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using Shouldly;

namespace WebApi.Test.Recipe.Register;

public class RegisterRecipeTest : MyRecipeBookClassFixture
{
    private readonly Guid _userId;
    private const string Method = "api/recipe";

    public RegisterRecipeTest(CustomWebApplicationFactory factory) : base(factory)
    {
       _userId = factory.GetUserId();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestRecipeJsonBuilder.Build();
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userId);
        
        var response = await DoPost(method: Method, request: request, token: token);
        
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        
        var responseData = await JsonDocument.ParseAsync(responseBody);
        
        var title =  responseData.RootElement.GetProperty("title").GetString();
        var id =  responseData.RootElement.GetProperty("id").GetString();
        
        title.ShouldNotBeNullOrWhiteSpace(); title.ShouldBe(request.Title);
        id.ShouldNotBeNullOrWhiteSpace();
    }
}