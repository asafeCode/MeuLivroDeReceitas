using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Application.Services.AutoMapper;
using MyRecipeBook.Application.UseCases.User.Register;

namespace MyRecipeBook.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddAutoMapper(services);
    }

    private static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddScoped(options => new AutoMapper.MapperConfiguration(
            options=>
        {
            options.AddProfile(new AutoMapping());
        }).CreateMapper());
    }


    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase,  RegisterUserUseCase>();
    }
    
}