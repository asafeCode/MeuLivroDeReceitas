using System.Globalization;
using System.Net;
using System.Text.Json;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;
using Shouldly;
using WebApi.Test.InlineData;

namespace WebApi.Test.User.ChangePassword;

public class ChangePasswordTest : MyRecipeBookClassFixture
{
    private readonly string _method = "api/user/change-password";
    private readonly Guid _userId;
    private readonly string _email;
    private readonly string _password;

    public ChangePasswordTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _userId = factory.GetUserId();
        _password = factory.GetPassword();
        _email = factory.GetEmail();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestChangeUserPasswordJsonBuilder.Build();
        request.Password = _password;

        var token = JwtTokenGeneratorBuilder.Build().Generate(_userId);

        var response = await DoPut(_method, request: request, token: token);

        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);

        var loginRequest = new RequestLoginJson()
        {
            Email = _email,
            Password = _password
        };
        
        var loginResponseUnauth = await DoPost(method:"api/login", loginRequest);
        loginResponseUnauth.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        
        loginRequest.Password = request.NewPassword;
        
        var loginResponseOk = await DoPost(method:"api/login", loginRequest);
        loginResponseOk.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_NewPassword_Empty(string culture)
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userId);
        
        var request = new RequestChangeUserPasswordJson()
        {
            Password = _password,
            NewPassword = string.Empty
        };
        
        var response = await DoPut(_method, request: request, token: token, culture: culture);
        
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        
        var responseData = await JsonDocument.ParseAsync(responseBody);

        var error = responseData.RootElement.GetProperty("errors").EnumerateArray();
        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("PASSWORD_EMPTY", new CultureInfo(culture));
        
        error.ShouldHaveSingleItem(); error.ShouldContain(jsonElement => jsonElement.GetString()!.Equals(expectedMessage));
    }
}