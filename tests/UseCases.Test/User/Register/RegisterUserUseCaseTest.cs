using System;
using System.Threading.Tasks;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Shouldly;
using Xunit;

namespace UseCases.Test.User.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var request = RequestUserRegisterJsonBuilder.Build();
        var useCase = CreateUseCase();
        var result = await useCase.Execute(request);
        
        result.ShouldNotBeNull();
        result.Name.ShouldBeSameAs(request.Name);
    }
    [Fact]
    public async Task Error_Email_Already_Registered()
    {
        var request = RequestUserRegisterJsonBuilder.Build();
        
        var useCase = CreateUseCase(request.Email);
        
        Func<Task> act = async () => await useCase.Execute(request);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
        
        exception.ErrorMessages.Count.ShouldBe(1);
        exception.ErrorMessages.ShouldContain(ResourceMessagesException.EMAIL_ALREADY_REGISTERED);
    }    
    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestUserRegisterJsonBuilder.Build();
        request.Name = String.Empty;
        
        var useCase = CreateUseCase();
        
        Func<Task> act = async () => await useCase.Execute(request);

        var exception = await act.ShouldThrowAsync<ErrorOnValidationException>();
        
        exception.ErrorMessages.Count.ShouldBe(1);
        exception.ErrorMessages.ShouldContain(ResourceMessagesException.NAME_EMPTY);
    }
    
    
    
    
    private static RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var readOnlyBuilder = new UserReadOnlyRepositoryBuilder();
        var writeOnly = UserWriteOnlyRepositoryBuilder.Build();   
        var mapper = MapperBuilder.Build();

        if (email.NotEmpty())
        {
            readOnlyBuilder.ExistsActiveUserWithEmail(email);
        }
        
        return new RegisterUserUseCase(readOnlyBuilder.Build(), writeOnly, mapper, passwordEncripter, unitOfWork);
    }
}