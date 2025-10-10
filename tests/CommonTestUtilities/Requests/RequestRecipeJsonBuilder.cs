using Bogus;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Enums;

namespace CommonTestUtilities.Requests;

public class RequestRecipeJsonBuilder
{
    public static RequestRecipeJson Build()
    {
        var step = 1;
        
        return new Faker<RequestRecipeJson>()
            .RuleFor(recipe => recipe.Title, faker => faker.Lorem.Word())
            .RuleFor(recipe => recipe.CookingTime, faker => faker.PickRandom<ComCookingTime>())
            .RuleFor(recipe => recipe.Difficulty, faker => faker.PickRandom<ComDifficulty>())
            .RuleFor(recipe => recipe.Ingredients, faker => faker.Make(3, () => faker.Commerce.Product()))
            .RuleFor(recipe => recipe.DishTypes, faker => faker.Make(3, faker.PickRandom<ComDishType>))
            .RuleFor(recipe => recipe.Instructions, faker => faker.Make(3, () => new RequestInstructionJson()
            {
                Text = faker.Lorem.Paragraph(),
                Step = step++
            }));
    }
}
