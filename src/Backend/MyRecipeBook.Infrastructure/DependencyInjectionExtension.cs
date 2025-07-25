using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        AddRepositories(services);
        AddDbContext_SqlServer(services);
        
    }

    private static void AddDbContext_SqlServer(this IServiceCollection services)
    {
        var connectionString = "Data Source=CTRL-SL2-4;Initial Catalog=meulivrodereceitas;User ID=sa;Password=@Password123;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;";
        
        services.AddDbContext<MyRecipeBookDbContext>(
            dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();

    }
}