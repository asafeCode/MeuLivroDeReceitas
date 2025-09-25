using Mapster;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Extensions;
using MyRecipeBook.Domain.Repositories.Recipe;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Domain.Services.LoggedUser;
using MyRecipeBook.Exceptions.ExceptionsBase;
using Sqids;

namespace MyRecipeBook.Application.UseCases.Recipe.Register;

public class RegisterRecipeUseCase : IRegisterRecipeUseCase
{
    private readonly ILoggedUser _loggedUser;
    private readonly IRecipeWriteOnlyRepository _writeOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly SqidsEncoder<long> _sqidsEncoder;

    public RegisterRecipeUseCase(
        ILoggedUser loggedUser,
        IRecipeWriteOnlyRepository writeOnlyRepository, 
        IUnitOfWork unitOfWork,
        SqidsEncoder<long> sqidsEncoder)
    {
        _loggedUser = loggedUser;
        _writeOnlyRepository = writeOnlyRepository;
        _unitOfWork = unitOfWork;
        _sqidsEncoder = sqidsEncoder;

    }

    public async Task<ResponseRegisteredRecipeJson> Execute(RequestRecipeJson request)
    {
        Validate(request);
        
        var loggedUser = await _loggedUser.User();
        
        var recipe = request.Adapt<Domain.Entities.Recipe>(TypeAdapterConfig.GlobalSettings);
        recipe.UserId = loggedUser.Id;
        
        var instructions = request.Instructions.OrderBy(i => i.Step).ToList();
        for(var index = 0; index < instructions.Count; index++)
            instructions.ElementAt(index).Step = index + 1;
        
        recipe.Instructions = instructions.Adapt<List<Instruction>>();
        
        await _writeOnlyRepository.Add(recipe);
        await _unitOfWork.Commit();

        return new ResponseRegisteredRecipeJson()
        {
            Id = _sqidsEncoder!.Encode(recipe.Id),
            Title = recipe.Title,
        };
    }

    private static void Validate(RequestRecipeJson request)
    {
        var validator = new RecipeValidator();
        var result = validator.Validate(request);
        
        if (result.IsValid.IsFalse())
            throw new ErrorOnValidationException(result.Errors.Select(e 
                => e.ErrorMessage).Distinct().ToList());
    }
}