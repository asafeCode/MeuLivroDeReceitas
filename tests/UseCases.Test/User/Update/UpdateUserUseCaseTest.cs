using CommonTestUtilities.Entities;
using CommonTestUtilities.LoggedUser;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Update;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.User.Update;

public class UpdateUserUseCaseTest 
{
    [Fact]
    public async Task Success()
    {
        var (user, _) = UserBuilder.Build();
        
        var request = RequestUpdateUserJsonBuilder.Build();
        
        var useCase = CreateUseCase(user);
        
        var act = async () => await useCase.Execute(request);
        
        await act.ShouldNotThrowAsync();
        
        user.Name.ShouldBe(request.NewName);
        user.Email.ShouldBe(request.NewEmail);
    }    
    
    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        var (user, _) = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        var useCase = CreateUseCase(user, request.NewEmail);
        var act = async () => await useCase.Execute(request);
        var exception =  await act.ShouldThrowAsync<ErrorOnValidationException>();
        
        exception.GetErrorMessage().Count.ShouldBe(1);
        exception.GetErrorMessage().ShouldContain(ResourceMessagesException.EMAIL_ALREADY_REGISTERED);
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var (user, _) = UserBuilder.Build();
        var request = RequestUpdateUserJsonBuilder.Build();
        request.NewName = string.Empty;
        
        var useCase = CreateUseCase(user);
        var act = async () => await useCase.Execute(request);
        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
        
        exception.GetErrorMessage().Count.ShouldBe(1);
        exception.GetErrorMessage().ShouldContain(ResourceMessagesException.NAME_EMPTY);
    }
    
    
    private static UpdateUserUseCase CreateUseCase(MyRecipeBook.Domain.Entities.User? user = null, string? email = null)
    {
        var loggedUser = LoggedUserBuilder.Build(user!);
        var readOnlyRepository =  new UserReadOnlyRepositoryBuilder();
        var repository = new UserUpdateOnlyRepositoryBuilder().GetById(user!).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();

        if (email.NotEmpty())
        {
            readOnlyRepository.ExistsActiveUserWithEmail(email);
        }
        
        return new UpdateUserUseCase(repository, loggedUser, readOnlyRepository.Build(), unitOfWork );
    }
}