using Moq;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Repositories.User;

namespace CommonTestUtilities.Repositories.Recipe;

public static class RecipeWriteOnlyRepositoryBuilder
{
    public static IRecipeWriteOnlyRepository Build()
    {
        var mock = new Mock<IRecipeWriteOnlyRepository>();
        return mock.Object;
    }
}