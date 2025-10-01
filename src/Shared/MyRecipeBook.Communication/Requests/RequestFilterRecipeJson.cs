using MyRecipeBook.Communication.Enums;

namespace MyRecipeBook.Communication.Requests;

public class RequestFilterRecipeJson
{
    public string? RecipeTitleIngredient { get; set; }
    public IList<ComCookingTime> CookingTime { get; set; } = [];
    public IList<ComDifficulty> Difficulty { get; set; } = [];
    public IList<ComDishType> DishTypes { get; set; } = [];
}