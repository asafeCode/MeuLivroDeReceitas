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

    private RegisterUserUseCase CreateUseCase()
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var readOnly = new UserReadOnlyRepositoryBuilder().Build();
        var writeOnly = UserWriteOnlyRepositoryBuilder.Build();   
        var mapper = MapperBuilder.Build();
        
        return new RegisterUserUseCase(readOnly, writeOnly, mapper, passwordEncripter, unitOfWork);
    }
}