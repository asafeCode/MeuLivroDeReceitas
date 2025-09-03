using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Requests;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;
using Shouldly;
using WebApi.Test.InlineData;

namespace WebApi.Test.Login.DoLogin;

public class DoLoginTest :  MyRecipeBookClassFixture
{
    private readonly string _method = "api/login";
    private readonly string _email;
    private readonly string _password;
    private readonly string _name;
    
    public DoLoginTest(CustomWebApplicationFactory factory) : base(factory)
    {
        _password = factory.GetPassword();
        _email = factory.GetEmail();
        _name = factory.GetName();
    }
    
    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson()
        {
            Email = _email,
            Password = _password,
        };

        var response = await DoPost(_method, request);
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        
        var responseData = await JsonDocument.ParseAsync(responseBody);
        
        var result = responseData.RootElement.GetProperty("name").GetString();
        
        result.ShouldNotBeNullOrWhiteSpace();
        result.ShouldBe(_name);
    }
    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Invalid_User(string culture)
    {
        var request = RequestLoginJsonBuilder.Build();
        
        var response = await DoPost(_method, request, culture);
        
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
        
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        
        var responseData = await JsonDocument.ParseAsync(responseBody);

        var error = responseData.RootElement.GetProperty("errors").EnumerateArray();
        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("EMAIL_OR_PASSWORD_INVALID", new CultureInfo(culture));
        
        error.ShouldSatisfyAllConditions(() =>
        {
            error.ShouldHaveSingleItem();
            error.ShouldContain(jsonElement => jsonElement.GetString()!.Equals(expectedMessage));
        });
        
    }

}
