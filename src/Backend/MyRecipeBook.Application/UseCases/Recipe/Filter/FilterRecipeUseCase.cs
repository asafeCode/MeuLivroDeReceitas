using Mapster;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Dtos;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions.ExceptionsBase;

namespace MyRecipeBook.Application.UseCases.Recipe.Filter;

public class FilterRecipeUseCase : IFilterRecipeUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IRecipeReadOnlyRepository _readOnlyRepository;
    public FilterRecipeUseCase(
        ILoggedUser loggedUser,
        IRecipeReadOnlyRepository readOnlyRepository)
    {
        _loggedUser = loggedUser;
        _readOnlyRepository = readOnlyRepository;
    }
    public async Task<ResponseRecipesJson> Execute(RequestFilterRecipeJson request)
    {
        Validate(request);

        var loggedUser = await _loggedUser.User();

        var filter = request.Adapt<FilterRecipesDto>();
        
        var recipes =  await _readOnlyRepository.Filter(loggedUser, filter);
        
        var response = new ResponseRecipesJson
        {
            Recipes = recipes.Adapt<List<ResponseShortRecipeJson>>()
        };
        
        return response;
    }

    private static void Validate(RequestFilterRecipeJson request)
    {
        var validator = new FilterRecipeValidator();
        var result = validator.Validate(request);

        if (result.IsValid.IsFalse())
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).Distinct().ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}