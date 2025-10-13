using Mapster;
using MyRecipeBook.Communication.Enums;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Enums;
using Sqids;

namespace MyRecipeBook.Application.Services.Mapper;

public static class MapConfigurations
{
    public static void Configure(SqidsEncoder<long> sqids)
    {
        TypeAdapterConfig<RequestUserRegisterJson, User>
            .NewConfig().Ignore(user => user.Password);
        
        TypeAdapterConfig<string, Ingredient>
            .NewConfig()
            .MapWith(dest => new Ingredient{Item = dest});

        TypeAdapterConfig<ComDishType, DishType>
            .NewConfig()
            .MapWith(dest => new DishType{Type = (DomDishType)dest});

        TypeAdapterConfig<RequestRecipeJson, Recipe>
            .NewConfig()
            .Ignore(dest => dest.Instructions)
            .Map(dest => dest.Ingredients, src => src.Ingredients.Distinct().Adapt<List<Ingredient>>())
            .Map(dest => dest.DishTypes, src => src.DishTypes.Distinct().Adapt<List<DishType>>());
            
        TypeAdapterConfig<Recipe, ResponseRegisteredRecipeJson>
            .NewConfig()
            .Map(dest => dest.Id, src => sqids.Encode(src.Id));
        
        TypeAdapterConfig<Recipe, ResponseShortRecipeJson>
            .NewConfig()
            .Map(dest => dest.Id, src => sqids.Encode(src.Id));

    }
}