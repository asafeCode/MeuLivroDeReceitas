using System.Globalization;
using System.Net;
using System.Text.Json;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using MyRecipeBook.Exceptions;
using Shouldly;
using WebApi.Test.InlineData;

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
    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Title_Empty(string culture)
    {
        var request = RequestRecipeJsonBuilder.Build();
        request.Title = string.Empty;
        
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userId);
        
        var response = await DoPost(method: Method, request: request, token: token, culture: culture);
        
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        
        var responseData = await JsonDocument.ParseAsync(responseBody);
        
        var error = responseData.RootElement.GetProperty("errors").EnumerateArray();
        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("RECIPE_TITLE_EMPTY", new CultureInfo(culture));
        
        error.ShouldHaveSingleItem(); error.ShouldContain(jsonElement => jsonElement.GetString()!.Equals(expectedMessage));
    }
}