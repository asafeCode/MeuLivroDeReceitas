using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.ChangePassword;
using MyRecipeBook.Application.UseCases.User.Update;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.User.ChangePassword;

public class ChangeUserPasswordUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var (user, password) = UserBuilder.Build();
        
        var request = RequestChangeUserPasswordJsonBuilder.Build();
        request.Password = password;
        
        var useCase = CreateUseCase(user);
        
        var act = async () => await useCase.Execute(request);
        
        await act.ShouldNotThrowAsync();
        
        var passwordEncripter = PasswordEncripterBuilder.Build().Encrypt(request.NewPassword);
        user.Password.ShouldBe(passwordEncripter);
    }    
    
    [Fact]
    public async Task Error_NewPassword_Empty()
    {
        var (user, password) = UserBuilder.Build();

        var request = new RequestChangeUserPasswordJson()
        {
            NewPassword = string.Empty,
            Password = password
        };
        
        var useCase = CreateUseCase(user);
        
        var act = async () => await useCase.Execute(request);
        
        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
        
        exception.GetErrorMessage().Count.ShouldBe(1);
        exception.GetErrorMessage().ShouldContain(ResourceMessagesException.PASSWORD_EMPTY);
    }    
    
    [Fact]
    public async Task Error_CurrentPassword_Different()
    {
        var (user, _) = UserBuilder.Build();
        
        var request = RequestChangeUserPasswordJsonBuilder.Build();
        
        var useCase = CreateUseCase(user);
        
        var act = async () => await useCase.Execute(request);
        
        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
        
        exception.GetErrorMessage().Count.ShouldBe(1);
        exception.GetErrorMessage().ShouldContain(ResourceMessagesException.PASSWORD_DIFFERENT_CURRENT_PASSWORD);
    } 
    
    
    private static ChangeUserPasswordUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User? user = null)
    {
        var loggedUser = LoggedUserBuilder.Build(user!);
        var repository = new UserUpdateOnlyRepositoryBuilder().GetById(user!).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        
        return new ChangeUserPasswordUseCase(loggedUser, passwordEncripter, repository, unitOfWork);
    }
}