using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Security.Tokens;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginUseCase : IDoLoginUseCase
{
    private readonly IUserReadOnlyRepository  _userRepository;
    private readonly PasswordEncripter  _passwordEncripter;
    private readonly IAccessTokenGenerator  _tokenGenerator;
    
    public DoLoginUseCase(IUserReadOnlyRepository userRepository, 
        PasswordEncripter passwordEncripter,
        IAccessTokenGenerator tokenGenerator)
    {
        _userRepository =  userRepository;
        _passwordEncripter = passwordEncripter;
        _tokenGenerator = tokenGenerator;
    }
    
    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var passwordEncripted = _passwordEncripter.Encrypt(request.Password);

        var user = await _userRepository.GetEmailAndPassword(request.Email, passwordEncripted)
                   ?? throw new InvalidLoginException();
        
        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            Tokens = new ResponseTokenJson
            {
                AccessToken = _tokenGenerator.Generate(user.UserId)
            }
        };
    }
}