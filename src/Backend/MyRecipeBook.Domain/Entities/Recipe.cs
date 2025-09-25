using System.ComponentModel.DataAnnotations.Schema;
using MyRecipeBook.Domain.Enums;

namespace MyRecipeBook.Domain.Entities;

[Table("Recipes")]
public class Recipe :  EntityBase
{
    public string Title { get; set; } = string.Empty;
    public DomCookingTime? CookingTime { get; set; }
    public DomDifficulty? Difficulty { get; set; }
    public IList<Ingredient> Ingredients { get; set; } = [];
    public IList<Instruction> Instructions { get; set; } = [];
    public IList<DishType> DishTypes { get; set; } = [];
    public long UserId { get; set; }
}
