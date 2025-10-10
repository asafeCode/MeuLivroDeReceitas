using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Recipe;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.Services.Mapper;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.Recipe.Register;

public class RegisterRecipeUseCaseTest
{
    public RegisterRecipeUseCaseTest()
    {
        MapConfigurations.Configure();
    }

    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        var request = RequestRecipeJsonBuilder.Build();
        var useCase = CreateUseCase(user);
        var response = await useCase.Execute(request);

        response.ShouldNotBeNull(); request.Title.ShouldNotBeNull(); 
        request.Title.ShouldBe(response.Title); response.Id.ShouldNotBeNullOrWhiteSpace();
    }    
    
    [Fact]
    public async Task Error_Title_Empty()
    {
        var (user, _) = UserBuilder.Build();
        
        var request = RequestRecipeJsonBuilder.Build();
        request.Title = string.Empty;
        
        var useCase = CreateUseCase(user);
        Func<Task> act = async () => await useCase.Execute(request);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
        
        exception.GetErrorMessage().Count.ShouldBe(1);
        exception.GetErrorMessage().ShouldContain(ResourceMessagesException.RECIPE_TITLE_EMPTY);
    }
    
    private static RegisterRecipeUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User? user = null)
    {
        var loggedUser = LoggedUserBuilder.Build(user!);
        var repository = RecipeWriteOnlyRepositoryBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var sqidsEncoder = IdRecipeEncripterBuilder.Build();    
        
        return new RegisterRecipeUseCase(loggedUser, repository, unitOfWork, sqidsEncoder);
    }
}