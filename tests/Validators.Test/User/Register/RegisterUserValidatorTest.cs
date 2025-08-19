﻿using CommonTestUtilities.Requests;
using Shouldly;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Exceptions;
using Xunit;

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
        var validator = new RegisterUserValidator();

        var request = RequestUserRegisterJsonBuilder.Build();
        request.Email = string.Empty;
        
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
        var validator = new RegisterUserValidator();

        var request = RequestUserRegisterJsonBuilder.Build();
        request.Email = "000.com";
        
        var result = validator.Validate(request);

        result.IsValid.ShouldBe(false);
        
        result.ShouldSatisfyAllConditions(() =>
            { 
                result.Errors.ShouldHaveSingleItem(); 
                result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_INVALID));
            }
        );


    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Error_Password_Invalid(int passwordLength)
    {
        var validator = new RegisterUserValidator();

        var request = RequestUserRegisterJsonBuilder.Build(passwordLength);
        
        var result = validator.Validate(request);

        result.IsValid.ShouldBe(false);
        
        result.ShouldSatisfyAllConditions(() =>
            { 
                result.Errors.ShouldHaveSingleItem(); 
                result.Errors.ShouldContain(e => e.ErrorMessage.Equals(ResourceMessagesException.PASSWORD_EMPTY));
            }
        );


    }
}