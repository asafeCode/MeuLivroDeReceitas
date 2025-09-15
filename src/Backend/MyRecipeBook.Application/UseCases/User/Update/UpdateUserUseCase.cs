using FluentValidation.Results;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Update;

public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    
    public UpdateUserUseCase(IUserUpdateOnlyRepository repository,
        ILoggedUser loggedUser,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _loggedUser = loggedUser;
        _userReadOnlyRepository = userReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestUpdateUserJson request)
    {
        var loggedUser = await _loggedUser.User();
        
        await Validate(request, loggedUser.Email);

        var user = await _repository.GetById(loggedUser.Id);

        user.Name = request.NewName;
        user.Email = request.NewEmail;
        
        _repository.Update(user);
        
        await _unitOfWork.Commit();
    }
    
    private async Task Validate(RequestUpdateUserJson request, string currentEmail)
    {
        var validator = new UpdateUserValidator();

        var result = await validator.ValidateAsync(request);

        if (result.IsValid && currentEmail.Equals(request.NewEmail).IsFalse())
        {
            var userExists = await _userReadOnlyRepository.ExistsActiveUserWithEmail(request.NewEmail);
            if (userExists)
            {
                result.Errors.Add(new ValidationFailure("email", ResourceMessagesException.EMAIL_ALREADY_REGISTERED)); 
            }
        }
        
        if (result.IsValid.IsFalse())
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}