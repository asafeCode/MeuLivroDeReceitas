using FluentValidation.Results;
using MyRecipeBook.Application.UseCases.User.Profile;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Cryptography;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.ChangePassword;

public class ChangeUserPasswordUseCase : IChangeUserPasswordUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IPasswordEncripter  _passwordEncripter;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IUnitOfWork  _unitOfWork;
    
    public ChangeUserPasswordUseCase(ILoggedUser loggedUser,
        IPasswordEncripter passwordEncripter,
        IUserUpdateOnlyRepository repository,
        IUnitOfWork unitOfWork)
    {
        _loggedUser = loggedUser;
        _passwordEncripter = passwordEncripter;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task Execute(RequestChangeUserPasswordJson request)
    {
        var loggedUser =  await _loggedUser.User();
        
        Validate(request, loggedUser);
        
        var user = await _repository.GetById(loggedUser.Id);
        
        var newPassword = _passwordEncripter.Encrypt(request.NewPassword);
        
        user.Password = newPassword;
        
        _repository.Update(user);
        
        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangeUserPasswordJson request, Domain.Entities.User  loggedUser)
    {
        var result = new ChangeUserPasswordValidator().Validate(request);
        var currentPassword = _passwordEncripter.Encrypt(request.Password);

        if (currentPassword.Equals(loggedUser.Password).IsFalse())
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.PASSWORD_DIFFERENT_CURRENT_PASSWORD)); 
        
        if (result.IsValid.IsFalse())
            throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
    }
}