using CommonTestUtilities.Requests;
using Shouldly;
using MyRecipeBook.Application.UseCases.User.Register;

namespace Validators.Test.User.Register;

public class RegisterUserValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterUserValidator();

        var request = RequestUserRegisterJsonBuilder.Build();
        
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBe(true);
    }
    
    [Fact]
       public void Error_Name_Empty()
    {
        var validator = new RegisterUserValidator();

        var request = RequestUserRegisterJsonBuilder.Build();
        request.Name = string.Empty;
        
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBe(false);
    }
}