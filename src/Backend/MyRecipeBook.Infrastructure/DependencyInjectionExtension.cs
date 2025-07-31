using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Repositories.User;
using MyRecipeBook.Infrastructure.DataAccess;
using MyRecipeBook.Infrastructure.DataAccess.Repositories;

namespace MyRecipeBook.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext_SqlServer(services, configuration);
        
    }

    private static void AddDbContext_SqlServer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ConnectionSqlServer");
        
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
        services.AddScoped<IUnitOfWork, UnitOfWork>();

    }
}