using MyRecipeBook.Communication.Enums;

namespace MyRecipeBook.Communication.Responses;

public class ResponseRecipeJson
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public IList<ResponseIngredientJson> Ingredients { get; set; } = [];
    public IList<ResponseInstructionJson> Instructions { get; set; } = [];
    public IList<ComDishType> DishTypes { get; set; } = [];
    public ComCookingTime? CookingTime { get; set; }
    public ComDifficulty? Difficulty { get; set; }
    public string? ImageUrl { get; set; }
}