using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using Shouldly;

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
    public async Task Email_Already_Exists()
    {
        var request = RequestUserRegisterJsonBuilder.Build();
        
        var useCase = CreateUseCase(request.Email);
        
        var result = await useCase.Execute(request);
    }
    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var readOnlyBuilder = new UserReadOnlyRepositoryBuilder();
        var writeOnly = UserWriteOnlyRepositoryBuilder.Build();   
        var mapper = MapperBuilder.Build();

        if (string.IsNullOrEmpty(email) == false)
        {
            readOnlyBuilder.ExistsActiveUserWithEmail(email);
        }
        
        return new RegisterUserUseCase(readOnlyBuilder.Build(), writeOnly, mapper, passwordEncripter, unitOfWork);
    }
}