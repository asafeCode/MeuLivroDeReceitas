using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using FluentValidation.Results;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly IMapper _mapper;
    private readonly PasswordEncripter _passwordEncripter;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(
        IUserReadOnlyRepository readOnlyRepository, 
        IUserWriteOnlyRepository writeOnlyRepository, 
        IMapper mapper,
        PasswordEncripter passwordEncripter,
        IUnitOfWork unitOfWork)
    {
        _readOnlyRepository = readOnlyRepository;
        _writeOnlyRepository = writeOnlyRepository;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _unitOfWork = unitOfWork;
        
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestUserRegisterJson request)
    {            
        
         await Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);// Instanciando a Classe em uma Variavel

        // Criptografia da Senha
        user.Password = _passwordEncripter.Encrypt(request.Password);  

        // Salvar no banco de Dados
        await _writeOnlyRepository.Add(user);
        await _unitOfWork.Commit();



        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            
        };
    }

    private async Task Validate(RequestUserRegisterJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);
        
        var emailExists = await _readOnlyRepository.ExistsActiveUserWithEmail(request.Email);

        if (emailExists)
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.EMAIL_ALREADY_REGISTERED));
        

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);

        }
    }
} 
