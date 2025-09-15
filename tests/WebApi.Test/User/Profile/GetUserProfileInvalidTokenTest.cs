using System.Globalization;
using System.Net;
using System.Text.Json;
using CommonTestUtilities.Tokens;
using MyRecipeBook.Exceptions;
using Shouldly;
using WebApi.Test.InlineData;

namespace WebApi.Test.User.Profile;

public class GetUserProfileInvalidTokenTest : MyRecipeBookClassFixture
{
    private readonly string _method = "api/user";
    public GetUserProfileInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory){}

    [Fact]
    public async Task Error_Token_Invalid()
    {
        var response = await DoGet(_method, token: "tokenInvalid");

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }  
    [Fact]
    public async Task Error_Token_Empty()
    {
        var response = await DoGet(_method, token: string.Empty);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }    
    [Fact]
    public async Task Token_With_User_Not_Found()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());
        var response = await DoGet(_method, token: token);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}


