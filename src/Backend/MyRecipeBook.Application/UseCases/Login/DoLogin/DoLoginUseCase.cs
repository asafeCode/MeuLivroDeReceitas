using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository  _userRepository;
    private readonly PasswordEncripter  _passwordEncripter;
    
    public DoLoginUseCase(IUserReadOnlyRepository userRepository, 
        PasswordEncripter passwordEncripter)
    {
        _userRepository =  userRepository;
        _passwordEncripter = passwordEncripter;
    }
    
    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var passwordEncripted = _passwordEncripter.Encrypt(request.Password);

        var user = await _userRepository.GetEmailAndPassword(request.Email, passwordEncripted)
                   ?? throw new InvalidLoginException();
        
        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
        };
    }
}