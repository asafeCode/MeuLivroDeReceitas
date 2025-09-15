using System.Net;
using System.Text.Json;
using CommonTestUtilities.Tokens;
using Shouldly;

namespace WebApi.Test.User.Profile;

public class GetUserProfileTest : MyRecipeBookClassFixture
{
    private readonly string _method = "api/user";
    private readonly string _email;
    private readonly string _name;
    private readonly Guid _userId;
    
    public GetUserProfileTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _email = factory.GetEmail();
        _name = factory.GetName();
        _userId = factory.GetUserId();
    }

    [Fact]
    public async Task Success()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userId);
        
        var response = await DoGet(_method, token: token);

        response.StatusCode.ShouldBe(HttpStatusCode.OK);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var name = responseData.RootElement.GetProperty("name").GetString();
        var email = responseData.RootElement.GetProperty("email").GetString();
        
        email.ShouldNotBeNullOrWhiteSpace(); email.ShouldBe(_email);
        name.ShouldNotBeNullOrWhiteSpace(); name.ShouldBe(_name);
    }
    
}