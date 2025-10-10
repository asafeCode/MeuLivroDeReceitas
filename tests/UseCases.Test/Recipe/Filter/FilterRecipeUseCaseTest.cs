using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.MapConfiguration;
using CommonTestUtilities.Repositories.Recipe;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.Recipe.Filter;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;
using Shouldly;

namespace UseCases.Test.Recipe.Filter;

public class FilterRecipeUseCaseTest : MapperForUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var request = RequestFilterRecipeJsonBuilder.Build();
        var useCase = CreateUseCase(user);
        var response = await useCase.Execute(request);
        
        response.ShouldNotBeNull();

    }    
    
    [Fact]
    public async Task Error_Title_Empty()
    {
        var (user, _) = UserBuilder.Build();
        
        var request = RequestFilterRecipeJsonBuilder.Build();
        
        var useCase = CreateUseCase(user);
        Func<Task> act = async () => await useCase.Execute(request);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
        
        exception.GetErrorMessage().Count.ShouldBe(1);
        exception.GetErrorMessage().ShouldContain(ResourceMessagesException.RECIPE_TITLE_EMPTY);
    }
    
    private static FilterRecipeUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User user,
        IList<MyRecipeBook.Domain.Entities.Recipe> recipes)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var repository = new RecipeReadOnlyRepositoryBuilder().Filter(user, recipes).Build();
        
        return new FilterRecipeUseCase(loggedUser, repository);
    }
}
