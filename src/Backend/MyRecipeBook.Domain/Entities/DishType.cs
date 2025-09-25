using System.ComponentModel.DataAnnotations.Schema;
using MyRecipeBook.Domain.Enums;

namespace MyRecipeBook.Domain.Entities;

[Table("DishTypes")]
public class DishType : EntityBase
{
    public DomDishType Type { get; set; }
    public long RecipeId { get; set; }
}
