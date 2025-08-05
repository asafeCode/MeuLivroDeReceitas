using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;

namespace Validators.Test.User.Register;

public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        // Instanciar o que ele precisa
        var validator = new RegisterUserValidator();

        var request = RequestUserRegisterJsonBuilder.Build();
        
        var result = validator.Validate(request);
        
        // Assert
        
        Assert.True(result.IsValid);
    }
}