using Moq;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Repositories.Recipe;

namespace CommonTestUtilities.Repositories.Recipe;

public class RecipeReadOnlyRepositoryBuilder
{
    private readonly Mock<IRecipeReadOnlyRepository> _repository;

    public RecipeReadOnlyRepositoryBuilder() => _repository = new Mock<IRecipeReadOnlyRepository>();

    
    public RecipeReadOnlyRepositoryBuilder Filter(MyRecipeBook.Domain.Entities.User user, IList<MyRecipeBook.Domain.Entities.Recipe> recipes)
    {
        _repository.Setup(repository => repository.Filter(user, It.IsAny<FilterRecipesDto>())).ReturnsAsync(recipes);
        return this;
    }
    
    
    public IRecipeReadOnlyRepository Build() =>  _repository.Object;
}