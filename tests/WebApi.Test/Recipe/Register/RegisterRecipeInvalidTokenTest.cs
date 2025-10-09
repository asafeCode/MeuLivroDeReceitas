using System.Net;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using Shouldly;

namespace WebApi.Test.Recipe.Register;

public class RegisterRecipeInvalidTokenTest : MyRecipeBookClassFixture
{
    private const string Method = "recipe";
    
    public RegisterRecipeInvalidTokenTest(CustomWebApplicationFactory factory) : base(factory) { }
    
    [Fact]
    public async Task Error_Token_Invalid()
    {
        var request = RequestRecipeJsonBuilder.Build();
        
        var response = await DoPost(Method, request: request, token: "tokenInvalid");

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }  
    [Fact]
    public async Task Error_Token_Empty()
    {
        var request = RequestRecipeJsonBuilder.Build();
        
        var response = await DoPost(Method, request: request, token: string.Empty);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }    
    [Fact]
    public async Task Token_With_User_Not_Found()
    {
        var request = RequestRecipeJsonBuilder.Build();
        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());
        
        var response = await DoPost(method: Method, request: request, token: token);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
}