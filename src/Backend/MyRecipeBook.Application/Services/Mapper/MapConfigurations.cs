using Mapster;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Communication.Responses;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Enums;
using Sqids;

namespace MyRecipeBook.Application.Services.Mapper;

public static class MapConfigurations
{
    public static void Configure()
    {
        TypeAdapterConfig<RequestUserRegisterJson, User>
            .NewConfig().Ignore(user => user.Password);

        TypeAdapterConfig<string, Ingredient>
            .NewConfig()
            .Map(dest => dest.Item, src => src);

        TypeAdapterConfig<ComDishType, DishType>
            .NewConfig()
            .Map(dest => dest.Type, src => src);

        TypeAdapterConfig<RequestRecipeJson, Recipe>
            .NewConfig()
            .Ignore(dest => dest.Instructions)
            .Map(dest => dest.Ingredients, src => src.Ingredients.Distinct()
                .Select(strItem => new Ingredient()
                {
                    Item = strItem
                }))
            
            .Map(dest => dest.DishTypes, src => src.DishTypes.Distinct()
                .Select(type => new DishType()
                {
                    Type = (DomDishType)type
                } ));
    }
}