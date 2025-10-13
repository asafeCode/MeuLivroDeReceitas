using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.Mapper;
using MyRecipeBook.Application.UseCases.Login.DoLogin;
using MyRecipeBook.Application.UseCases.Recipe.Filter;
using MyRecipeBook.Application.UseCases.Recipe.Register;
using MyRecipeBook.Application.UseCases.User.ChangePassword;
using MyRecipeBook.Application.UseCases.User.Profile;
using MyRecipeBook.Application.UseCases.User.Register;
using MyRecipeBook.Application.UseCases.User.Update;
using Sqids;

namespace MyRecipeBook.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services,  IConfiguration configuration)
    {
        AddSqidsEncoderAndMapper(services, configuration);
        AddUseCases(services);
    }

    private static void AddSqidsEncoderAndMapper(this IServiceCollection services, IConfiguration configuration)
    {
        var alphabet = configuration.GetValue<string>("Settings:IdCriptographyAlphabet");
        var sqids = new SqidsEncoder<long>(new SqidsOptions
        {
            MinLength = 3,
            Alphabet = alphabet!
        });
        MapConfigurations.Configure(sqids);
        services.AddScoped<SqidsEncoder<long>>(s => sqids);
    }
    
    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
        services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
        services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
        services.AddScoped<IChangeUserPasswordUseCase, ChangeUserPasswordUseCase>();
        services.AddScoped<IRegisterRecipeUseCase, RegisterRecipeUseCase>();
        services.AddScoped<IFilterRecipeUseCase, FilterRecipeUseCase>();
    } 
}