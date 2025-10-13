using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.MapConfiguration;
using CommonTestUtilities.Repositories.Recipe;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.Recipe.Filter;
using MyRecipeBook.Communication.Enums;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.Recipe.Filter;
public class FilterRecipeUseCaseTest : MapperForUseCaseTests
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var request = RequestFilterRecipeJsonBuilder.Build();
        var recipes = RecipeBuilder.Collection(user);
        var useCase = CreateUseCase(user, recipes);

        var result = await useCase.Execute(request);

        result.ShouldNotBeNull();
        result.Recipes.ShouldNotBeNull();
        result.Recipes.Count.ShouldBe(recipes.Count);
    }

    [Fact]
    public async Task Error_CookingTime_Invalid()
    {
        var (user, _) = UserBuilder.Build();
        var recipes = RecipeBuilder.Collection(user);
        var request = RequestFilterRecipeJsonBuilder.Build();
        request.CookingTime.Add((ComCookingTime)1000);
        var useCase = CreateUseCase(user, recipes);

        Func<Task> act = async () => await useCase.Execute(request);
        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
        
        exception.GetErrorMessage().Count.ShouldBe(1);
        exception.GetErrorMessage().ShouldContain(ResourceMessagesException.COOKING_TIME_NOT_SUPPORTED);
    }

    private static FilterRecipeUseCase CreateUseCase(
        MyRecipeBook.Domain.Entities.User user,
        IList<MyRecipeBook.Domain.Entities.Recipe> recipes)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var repository = new RecipeReadOnlyRepositoryBuilder().Filter(user, recipes).Build();

        return new FilterRecipeUseCase(loggedUser, repository);
    }
}