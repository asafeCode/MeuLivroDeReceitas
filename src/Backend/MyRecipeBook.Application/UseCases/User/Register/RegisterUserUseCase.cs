using FluentValidation.Results;
using Mapster;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Cryptography;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _tokenGenerator;

    public RegisterUserUseCase(
        IUserReadOnlyRepository readOnlyRepository, 
        IUserWriteOnlyRepository writeOnlyRepository, 
        IPasswordEncripter passwordEncripter,
        IAccessTokenGenerator tokenGenerator,
        IUnitOfWork unitOfWork)
    {
        _readOnlyRepository = readOnlyRepository;
        _writeOnlyRepository = writeOnlyRepository;
        _passwordEncripter = passwordEncripter;
        _tokenGenerator = tokenGenerator;
        _unitOfWork = unitOfWork;
        
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestUserRegisterJson request)
    {            
        await Validate(request);
        
        var user = request.Adapt<Domain.Entities.User>();
        user.Password = _passwordEncripter.Encrypt(request.Password);
        
        user.UserId = Guid.NewGuid();

        await _writeOnlyRepository.Add(user);
        
        await _unitOfWork.Commit();
        
        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokenJson
            {
                AccessToken = _tokenGenerator.Generate(user.UserId)
            }
            
        };
    }

    private async Task Validate(RequestUserRegisterJson request)
    {
        var validator = new RegisterUserValidator();

        var result = await validator.ValidateAsync(request);
        
        var emailExists = await _readOnlyRepository.ExistsActiveUserWithEmail(request.Email);

        if (emailExists)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
        

        if (result.IsValid.IsFalse())
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);

        }
    }
} 
