using System.Data;
using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Update;

public class UpdateUserValidator : AbstractValidator<RequestUpdateUserJson>
{
    public UpdateUserValidator()
    {
        RuleFor(request => request.NewName).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        RuleFor(request => request.NewEmail).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
        
        When(request => request.NewEmail.NotEmpty(), () =>
        {
            RuleFor(request => request.NewEmail).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
        });
    }
    
}