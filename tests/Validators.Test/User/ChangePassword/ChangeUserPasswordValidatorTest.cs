using CommonTestUtilities.Requests;
using Microsoft.Identity.Client;
using MyRecipeBook.Application.UseCases.User.ChangePassword;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;
using Shouldly;

namespace Validators.Test.User.ChangePassword;

public class ChangeUserPasswordValidatorTest
{
    [Fact]
    public void Success()
    {
        var validator = new ChangeUserPasswordValidator();
        var request = RequestChangeUserPasswordJsonBuilder.Build();
        
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeTrue(); 
    }

    [Fact]
    public void Error_Password_Empty()
    {
        var validator = new ChangeUserPasswordValidator();
        var request = RequestChangeUserPasswordJsonBuilder.Build();
        request.NewPassword =  string.Empty;
        
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBeFalse(); result.Errors.ShouldHaveSingleItem(); 
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.PASSWORD_EMPTY));
    } 
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Password_Invalid(int passwordLenght)
    {
        var validator = new ChangeUserPasswordValidator();

        var request = RequestChangeUserPasswordJsonBuilder.Build(passwordLenght);
        
        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse(); result.Errors.ShouldHaveSingleItem(); 
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.INVALID_PASSWORD));
    }
}
