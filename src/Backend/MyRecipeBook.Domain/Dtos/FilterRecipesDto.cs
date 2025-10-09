using MyRecipeBook.Domain.Enums;

namespace MyRecipeBook.Domain.Dtos;

public record FilterRecipesDto
{
    public string? RecipeTitleIngredient { get; set; }
    public IList<DomCookingTime> CookingTime { get; set; } = [];
    public IList<DomDifficulty> Difficulty { get; set; } = [];
    public IList<DomDishType> DishTypes { get; set; } = [];
}