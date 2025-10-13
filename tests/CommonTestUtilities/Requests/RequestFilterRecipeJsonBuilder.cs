using Bogus;
using MyRecipeBook.Communication.Enums;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Entities;

namespace CommonTestUtilities.Requests;

public static class RequestFilterRecipeJsonBuilder
{
    public static RequestFilterRecipeJson Build()
    {
        return new Faker<RequestFilterRecipeJson>()
            .RuleFor(request => request.RecipeTitleIngredient, f => f.Lorem.Word())
            .RuleFor(request => request.CookingTime, f => f.Make(1, f.PickRandom<ComCookingTime>))
            .RuleFor(request => request.Difficulty, f => f.Make(1, f.PickRandom<ComDifficulty>))
            .RuleFor(request => request.DishTypes, f => f.Make(1, f.PickRandom<ComDishType>));
    }
}