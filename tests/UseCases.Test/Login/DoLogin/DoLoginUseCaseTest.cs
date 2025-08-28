using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.Login.DoLogin;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.Login.DoLogin;

public class DoLoginUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, password) = UserBuilder.Build();
        var useCase = CreateUseCase(user); var result = await useCase.Execute(new RequestLoginJson
        {
            Email = user.Email,
            Password = password
        });
        
        
        result.ShouldNotBeNull().ShouldSatisfyAllConditions(() =>
        {
            result.Name.ShouldNotBeNullOrWhiteSpace();
            result.Name.ShouldBe(user.Name);
        });
    }
    
    [Fact]
    public async Task Error_Invalid_User()
    {
        var request = RequestLoginJsonBuilder.Build();
        
        var useCase = CreateUseCase();
        
        Func<Task> act = async () => await useCase.Execute(request);

        var exception = await act.ShouldThrowAsync<InvalidLoginException>();
        
        exception.GetErrorMessages().Count.ShouldBe(1);
        exception.GetErrorMessages().ShouldContain(ResourceMessagesException.EMAIL_OR_PASSWORD_INVALID);
    }


    private static DoLoginUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User? user = null)
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var readOnlyBuilder = new UserReadOnlyRepositoryBuilder();

        if (user is not null)
        {
            readOnlyBuilder.GetEmailAndPassword(user);
        }
        
        return new DoLoginUseCase(readOnlyBuilder.Build(), passwordEncripter);
    }
}