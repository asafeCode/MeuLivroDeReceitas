using Mapster;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Recipe.GetById;

public class GetRecipeByIdUseCase : IGetRecipeByIdUseCase
{
    private readonly IRecipeReadOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;

    public GetRecipeByIdUseCase(
        IRecipeReadOnlyRepository repository,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _loggedUser = loggedUser;
    }
    
    public async Task<ResponseRecipeJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.User();
        
        var recipe = await _repository.GetById(loggedUser, id);
        
        if (recipe is null)
        {
            throw new NotFoundException(ResourceMessagesException.RECIPE_NOT_FOUND);
        }
        
        return recipe.Adapt<ResponseRecipeJson>();
    }
}