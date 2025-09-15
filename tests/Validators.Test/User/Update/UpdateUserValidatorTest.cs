using CommonTestUtilities.Requests;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Application.UseCases.User.Update;
using MyRecipeBook.Exceptions;
using Shouldly;

namespace Validators.Test.User.Update;

public class UpdateUserValidatorTest
{
     [Fact]
    public void Success()
    {
        var validator = new UpdateUserValidator();

        var request = RequestUpdateUserJsonBuilder.Build();
        
        var result = validator.Validate(request);
        
        result.IsValid.ShouldBe(true);
    }
    
    [Fact]
    public void Error_Name_Empty()
    {
        var validator = new UpdateUserValidator();

        var request = RequestUpdateUserJsonBuilder.Build();
        request.NewName = string.Empty;
        
        var result = validator.Validate(request);

        result.IsValid.ShouldBe(false);
        
        result.ShouldSatisfyAllConditions(() =>
            { 
                result.Errors.ShouldHaveSingleItem(); 
                result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.NAME_EMPTY));
            }
        );


    }
    
    [Fact]
    public void Error_Email_Empty()
    {
        var request = RequestUpdateUserJsonBuilder.Build();
        
        var validator = new UpdateUserValidator();

        request.NewEmail = string.Empty;
        
        var result = validator.Validate(request);

        result.IsValid.ShouldBe(false);
        
        result.ShouldSatisfyAllConditions(() =>
            { 
                result.Errors.ShouldHaveSingleItem(); 
                result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_EMPTY));
            }
        );


    }    
    
    [Fact]
    public void Error_Email_Invalid()
    {
        var validator = new UpdateUserValidator();

        var request = RequestUpdateUserJsonBuilder.Build();
        request.NewEmail = "000.com";
        
        var result = validator.Validate(request);

        result.IsValid.ShouldBe(false);
        
        result.ShouldSatisfyAllConditions(() =>
            { 
                result.Errors.ShouldHaveSingleItem(); 
                result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_INVALID));
            }
        );
    }
}