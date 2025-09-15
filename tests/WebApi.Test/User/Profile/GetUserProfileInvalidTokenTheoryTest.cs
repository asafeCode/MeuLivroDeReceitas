﻿using System.Globalization;
using System.Net;
using System.Text.Json;
using CommonTestUtilities.Tokens;
using MyRecipeBook.Exceptions;
using Shouldly;
using WebApi.Test.InlineData;

namespace WebApi.Test.User.Profile;

public class GetUserProfileInvalidTokenTheoryTest : MyRecipeBookClassFixture
{
    private readonly string _method = "api/user";

    public GetUserProfileInvalidTokenTheoryTest(CustomWebApplicationFactory factory) : base(factory) {}
    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Token_Invalid(string culture)
    {
        var response = await DoGet(_method, token: "tokenInvalid", culture);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);
        
        var error = responseData.RootElement.GetProperty("errors").EnumerateArray();
        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("USER_WITHOUT_PERMISSION_ACCESS_RESOURCE", new CultureInfo(culture));
        
        error.ShouldSatisfyAllConditions(() =>
        {
            error.ShouldHaveSingleItem();
            error.ShouldContain(jsonElement => jsonElement.GetString()!.Equals(expectedMessage));
        });
    }    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Token_Empty(string culture)
    {
        var response = await DoGet(_method, token: string.Empty, culture);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);
        
        var error = responseData.RootElement.GetProperty("errors").EnumerateArray();
        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("NO_TOKEN", new CultureInfo(culture));
        
        error.ShouldSatisfyAllConditions(() =>
        {
            error.ShouldHaveSingleItem();
            error.ShouldContain(jsonElement => jsonElement.GetString()!.Equals(expectedMessage));
        });
    }    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Token_With_User_Not_Found(string culture)
    {
        var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());
        
        var response = await DoGet(_method, token: token, culture);

        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);
        
        var error = responseData.RootElement.GetProperty("errors").EnumerateArray();
        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("USER_WITHOUT_PERMISSION_ACCESS_RESOURCE", new CultureInfo(culture));
        
        error.ShouldSatisfyAllConditions(() =>
        {
            error.ShouldHaveSingleItem();
            error.ShouldContain(jsonElement => jsonElement.GetString()!.Equals(expectedMessage));
        });
    }
    
}