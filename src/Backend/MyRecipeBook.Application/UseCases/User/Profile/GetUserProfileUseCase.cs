using Mapster;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Services.LoggedUser;

namespace MyRecipeBook.Application.UseCases.User.Profile;

public class GetUserProfileUseCase : IGetUserProfileUseCase
{
    private readonly ILoggedUser _loggedUser;
    public GetUserProfileUseCase(ILoggedUser loggedUser)
    {
        _loggedUser = loggedUser;
    }
    
    public async Task<ResponseUserProfileJson> Execute()
    {
        var user = await _loggedUser.User();
        
        return user.Adapt<ResponseUserProfileJson>();
    }
}