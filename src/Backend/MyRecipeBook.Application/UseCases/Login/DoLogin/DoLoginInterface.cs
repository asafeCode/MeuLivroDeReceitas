using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.User;

namespace MyRecipeBook.Application.UseCases.Login.DoLogin;

public class DoLoginInterface (
    IUserReadOnlyRepository  repository,
    PasswordEncripter  passwordEncripter):  IDoLoginUseCase
{
    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        var encriptedPassword = passwordEncripter.Encrypt(request.Password); 
        var user = await repository.GetEmailAndPassword(request.Email, encriptedPassword);
        
        
        return new ResponseRegisteredUserJson
        {
            Name = ""
        };
    }
}