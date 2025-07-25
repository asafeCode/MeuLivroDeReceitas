using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;
using System.ComponentModel.DataAnnotations;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserUseCase
{
    private readonly IUserReadOnlyRepository _ReadOnlyRepository;
    private readonly IUserWriteOnlyRepository _WriteOnlyRepository;

    public async Task<ResponseRegisteredUserJson> Execute(RequestUserRegisterJson request)
    {            
        var passwordEncripter = new PasswordEncripter();
        var autoMapper = new AutoMapper.MapperConfiguration(options =>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper();

        Validate(request);

        var user = autoMapper.Map<Domain.Entities.User>(request);// Instanciando a Classe em uma Variavel

        // Criptografia da Senha
        user.Password = passwordEncripter.Encrypt(request.Password);

        // Salvar no banco de Dados
        await _WriteOnlyRepository.Add(user);



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
