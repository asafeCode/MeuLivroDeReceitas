using System.Net;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using MyRecipeBook.Communication.Requests;
using Shouldly;

namespace WebApi.Test.User.ChangePassword;

public class ChangePasswordInvalidTokenTest : MyRecipeBookClassFixture
{
    private readonly string _method = "api/user";
    public ChangePasswordInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory){}

    [Fact]
    public async Task Error_Token_Invalid()
    {
        var request = new RequestChangeUserPasswordJson();
        var response = await DoPut(_method, request ,token: "tokenInvalid");

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }  
    
    [Fact]
    public async Task Error_Token_Empty()
    {
        var request = new RequestChangeUserPasswordJson();
        var response = await DoPut(_method, request ,token: string.Empty);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }    
    
    [Fact]
    public async Task Token_With_User_Not_Found()
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());
        var request = new RequestChangeUserPasswordJson();
        var response = await DoPut(_method, request ,token: token);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}