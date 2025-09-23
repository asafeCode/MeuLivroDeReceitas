using Mapster;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Application.Services.Mapper;

public static class MapConfigurations
{
    public static void Configure()
    {
        TypeAdapterConfig<RequestUserRegisterJson, User>
            .NewConfig()
            .Ignore(user => user.Password);
    }
}