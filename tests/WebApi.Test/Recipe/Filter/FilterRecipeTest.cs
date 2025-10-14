using System.Globalization;
using System.Net;
using System.Text.Json;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using MyRecipeBook.Communication.Enums;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Enums;
using MyRecipeBook.Exceptions;
using Shouldly;
using WebApi.Test.InlineData;

namespace WebApi.Test.Recipe.Filter;


public class FilterRecipeTest : MyRecipeBookClassFixture
{
    private readonly Guid _userId;
    private const string Method = "recipe/filter";
    private readonly string _recipeTitle;
    private readonly DomCookingTime _cookingTime;
    private readonly DomDifficulty _difficulty;
    private readonly IList<DomDishType> _dishType;
    
    

    public FilterRecipeTest(CustomWebApplicationFactory factory) : base(factory)
    {
       _userId = factory.GetUserId();
       _recipeTitle = factory.GetRecipeTitle();
       _cookingTime = factory.GetRecipeCookingTime();
       _difficulty = factory.GetRecipeDifficulty();
       _dishType = factory.GetDishTypes();
    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestFilterRecipeJson
        {
            RecipeTitleIngredient = _recipeTitle,
            CookingTime = [(ComCookingTime)_cookingTime],
            Difficulty = [(ComDifficulty)_difficulty],
            DishTypes = _dishType.Select(dishType => (ComDishType)dishType).ToList()
        };
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userId);
        
        var response = await DoPost(method: Method, request: request, token: token);
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        
        var responseData = await JsonDocument.ParseAsync(responseBody);
        
        var recipes = responseData.RootElement.GetProperty("recipes").EnumerateArray();
        
        recipes.ShouldNotBeEmpty();
    }      
    
    [Fact]
    public async Task Success_NoContent()
    {
        var request = RequestFilterRecipeJsonBuilder.Build();
        request.RecipeTitleIngredient = "RecipeNoExists";
        
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userId);
        
        var response = await DoPost(method: Method, request: request, token: token);
        
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
    }    
    
    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_CookingTime_Invalid(string culture)
    {
        var request = RequestFilterRecipeJsonBuilder.Build();
        request.CookingTime.Add((ComCookingTime)1000);
        
        var token = JwtTokenGeneratorBuilder.Build().Generate(_userId);
        
        var response = await DoPost(method: Method, request: request, token: token, culture: culture);
        
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        
        await using var responseBody = await response.Content.ReadAsStreamAsync();
        
        var responseData = await JsonDocument.ParseAsync(responseBody);
        
        var error = responseData.RootElement.GetProperty("errors").EnumerateArray();
        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("COOKING_TIME_NOT_SUPPORTED", new CultureInfo(culture));
        
        error.ShouldHaveSingleItem(); error.ShouldContain(jsonElement => jsonElement.GetString()!.Equals(expectedMessage));
    }
}
