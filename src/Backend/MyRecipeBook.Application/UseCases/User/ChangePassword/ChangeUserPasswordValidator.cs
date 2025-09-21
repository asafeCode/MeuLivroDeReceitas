using FluentValidation;
using MyRecipeBook.Application.SharedValidators;
using MyRecipeBook.Application.UseCases.User.Update;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.ChangePassword;

public class ChangeUserPasswordValidator : AbstractValidator<RequestChangeUserPasswordJson>
{
    public ChangeUserPasswordValidator()
    {
        RuleFor(request => request.NewPassword).SetValidator(new PasswordValidator<RequestChangeUserPasswordJson>());
    }
}