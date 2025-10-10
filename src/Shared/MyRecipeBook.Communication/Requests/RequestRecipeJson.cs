using MyRecipeBook.Domain.Enums;

namespace MyRecipeBook.Communication.Requests;

public class RequestRecipeJson
{
    public string Title { get; set; } = string.Empty;
    public ComCookingTime? CookingTime { get; set; }
    public ComDifficulty? Difficulty { get; set; }
    public IList<string> Ingredients { get; set; } = [];
    public IList<RequestInstructionJson> Instructions { get; set; } = [];
    public IList<ComDishType> DishTypes { get; set; } = [];
}