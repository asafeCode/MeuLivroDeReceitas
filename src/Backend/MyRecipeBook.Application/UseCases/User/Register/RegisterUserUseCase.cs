using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IUserWriteOnlyRepository _writeOnlyRepository;
    private readonly Mapper _mapper;

    public RegisterUserUseCase(
        IUserReadOnlyRepository readOnlyRepository, 
        IUserWriteOnlyRepository writeOnlyRepository, 
        Mapper mapper)
    {
        _readOnlyRepository = readOnlyRepository;
        _writeOnlyRepository = writeOnlyRepository;
        _mapper = mapper;
        
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestUserRegisterJson request)
    {            
        var passwordEncripter = new PasswordEncripter();
        
        Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);// Instanciando a Classe em uma Variavel

        // Criptografia da Senha
        user.Password = passwordEncripter.Encrypt(request.Password);

        // Salvar no banco de Dados
        await _writeOnlyRepository.Add(user);



        return new ResponseRegisteredUserJson
        {
            Name = request.Name,
            
        };
    }

    private void Validate(RequestUserRegisterJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);

        }
    }
} 
