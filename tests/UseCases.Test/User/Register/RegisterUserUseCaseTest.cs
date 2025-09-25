using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Tokens;
using MyRecipeBook.Application.Services.Mapper;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Shouldly;

namespace UseCases.Test.User.Register;

public class RegisterUserUseCaseTest
{
    public RegisterUserUseCaseTest()
    {
        MapConfigurations.Configure();
    }
    [Fact]
    public async Task Success()
    {
        var request = RequestUserRegisterJsonBuilder.Build();
        var useCase = CreateUseCase();
        var result = await useCase.Execute(request);
        
        result.ShouldNotBeNull().ShouldSatisfyAllConditions(() =>
        {
            result.Name.ShouldBeSameAs(request.Name); result.Name.ShouldNotBeNullOrWhiteSpace();
            result.Tokens.ShouldNotBeNull(); result.Tokens.AccessToken.ShouldNotBeNullOrEmpty();
        });
    }
    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        var request = RequestUserRegisterJsonBuilder.Build();
        
        var useCase = CreateUseCase(request.Email);
        
        Func<Task> act = async () => await useCase.Execute(request);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
        
        exception.GetErrorMessage().Count.ShouldBe(1);
        exception.GetErrorMessage().ShouldContain(ResourceMessagesException.EMAIL_ALREADY_REGISTERED);
    }    
    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestUserRegisterJsonBuilder.Build();
        request.Name = string.Empty;
        
        var useCase = CreateUseCase();
        
        Func<Task> act = async () => await useCase.Execute(request);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
        
        exception.GetErrorMessage().Count.ShouldBe(1);
        exception.GetErrorMessage().ShouldContain(ResourceMessagesException.NAME_EMPTY);
    }
    
    
    
    
    private static RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var readOnlyBuilder = new UserReadOnlyRepositoryBuilder();
        var accessTokenGenerator = JwtTokenGeneratorBuilder.Build();
        var writeOnly = UserWriteOnlyRepositoryBuilder.Build();   

        if (email.NotEmpty())
        {
            readOnlyBuilder.ExistsActiveUserWithEmail(email);
        }
        
        return new RegisterUserUseCase(readOnlyBuilder.Build(), writeOnly, passwordEncripter, accessTokenGenerator, unitOfWork);
    }
}