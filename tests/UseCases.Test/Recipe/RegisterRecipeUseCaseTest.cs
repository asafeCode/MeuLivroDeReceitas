using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Repositories.Recipe;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.Services.Mapper;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using MyRecipeBook.Communication.Requests;
using Shouldly;
using Sqids;

namespace UseCases.Test.Recipe;

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
        var result = await useCase.Execute(request);
        
        result.ShouldNotBeNull().ShouldSatisfyAllConditions(() =>
        {
            result.Title.ShouldBeSameAs(request.Title); result.Title.ShouldNotBeNullOrWhiteSpace();
            result.Id.ShouldNotBeNull();
        });
    }
    
    private static RegisterRecipeUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User? user = null)
    {
        var loggedUser = LoggedUserBuilder.Build(user!);
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeOnly = RecipeWriteOnlyRepositoryBuilder.Build();
        
        return new RegisterRecipeUseCase(loggedUser, writeOnly, unitOfWork, new SqidsEncoder<long>());
    }
}