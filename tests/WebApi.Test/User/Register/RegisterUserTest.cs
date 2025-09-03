using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using CommonTestUtilities.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using MyRecipeBook.Exceptions;
using Shouldly;
using WebApi.Test.InlineData;
using Xunit;

namespace WebApi.Test.User.Register;

public class RegisterUserTest : MyRecipeBookClassFixture
{
    private readonly string _method = "api/user";
    
    public RegisterUserTest(CustomWebApplicationFactory factory) : base(factory) {}

    [Fact]
    public async Task Success()
    {
        var request = RequestUserRegisterJsonBuilder.Build();

        var response = await DoPost(_method, request);
        
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
    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Name_Empty(string culture)
    {
        var request = RequestUserRegisterJsonBuilder.Build();
        request.Name = string.Empty;
        
        var response = await DoPost(_method, request, culture);
        
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        
        var responseData = await JsonDocument.ParseAsync(responseBody);

        var error = responseData.RootElement.GetProperty("errors").EnumerateArray();
        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("NAME_EMPTY", new CultureInfo(culture));
        
        error.ShouldSatisfyAllConditions(() =>
        {
            error.ShouldHaveSingleItem();
            error.ShouldContain(jsonElement => jsonElement.GetString()!.Equals(expectedMessage));
        });
    }

}