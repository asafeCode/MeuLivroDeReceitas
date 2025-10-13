using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories.Recipe;

namespace MyRecipeBook.Infrastructure.DataAccess.Repositories;

public class RecipeRepository : IRecipeWriteOnlyRepository, IRecipeReadOnlyRepository
{
    private readonly MyRecipeBookDbContext _dbContext;
    public RecipeRepository(MyRecipeBookDbContext dbContext) => _dbContext = dbContext;
    
    public async Task Add(Recipe recipe) => await _dbContext.Recipes.AddAsync(recipe);

    public async Task<IList<Recipe>> Filter(User user, FilterRecipesDto filters)
    {
        var query = _dbContext.Recipes
            .AsNoTracking()
            .Include(rec => rec.Ingredients)
            .Where(recipe => recipe.Active && recipe.UserId == user.Id);

        if (filters.Difficulty.Any())
        {
            query = query.Where(recipe => recipe.Difficulty.HasValue && filters.Difficulty.Contains(recipe.Difficulty.Value));
        }  
        if (filters.CookingTime.Any())
        {
            query = query.Where(recipe => recipe.CookingTime.HasValue && filters.CookingTime.Contains(recipe.CookingTime.Value));
        }  
        if (filters.DishTypes.Any())
        {
            query = query.Where(recipe => recipe.DishTypes.Any(dishType => filters.DishTypes.Contains(dishType.Type)));
        }

        if (filters.RecipeTitleIngredient.NotEmpty())
        {
            query = query.Where(recipe => recipe.Title.Contains(filters.RecipeTitleIngredient)
            || recipe.Ingredients.Any(ingredient => ingredient.Item.Contains(filters.RecipeTitleIngredient)));
        }
        
        return await query.ToListAsync();
    }

    public async Task<Recipe?> GetById(User user, long id)
    {
        return await _dbContext
            .Recipes
            .AsNoTracking()
            .Include(rec => rec.Ingredients)
            .Include(rec => rec.Instructions)
            .Include(rec => rec.DishTypes)
            .FirstOrDefaultAsync(recipe => recipe.Active && recipe.Id == id && recipe.UserId == user.Id );
    }
}