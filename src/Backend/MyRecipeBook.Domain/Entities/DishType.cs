using MyRecipeBook.Domain.Enums;

namespace MyRecipeBook.Domain.Entities;

public class DishType : EntityBase
{
    public DomDishType Type { get; set; }
    public long RecipeId { get; set; }
}
