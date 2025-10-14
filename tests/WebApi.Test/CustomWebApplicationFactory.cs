using System.Linq;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Enums;
using MyRecipeBook.Infrastructure.DataAccess;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private string _password = string.Empty;
    private MyRecipeBook.Domain.Entities.User _user = null!;
    private MyRecipeBook.Domain.Entities.Recipe _recipe = null!;
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(desc =>
                    desc.ServiceType == typeof(DbContextOptions<MyRecipeBookDbContext>));
                
                if (descriptor is not null)
                    services.Remove(descriptor);

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<MyRecipeBookDbContext>(opt =>
                {
                    opt.UseInMemoryDatabase("InMemoryDbForTesting");
                    opt.UseInternalServiceProvider(provider);
                });
                
                using var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<MyRecipeBookDbContext>();
                dbContext.Database.EnsureDeleted();

                StartDatabase(dbContext);
            });


    }
    public string GetEmail() => _user.Email;
    public string GetPassword() => _password;
    public string GetName() => _user.Name;
    public Guid GetUserId() => _user.UserId;
    
    public string GetRecipeId() => IdRecipeEncripterBuilder.Build().Encode(_recipe.Id);
    public string GetRecipeTitle() => _recipe.Title;
    public DomDifficulty GetRecipeDifficulty() => _recipe.Difficulty!.Value;
    public DomCookingTime GetRecipeCookingTime() => _recipe.CookingTime!.Value;
    public IList<DomDishType> GetDishTypes() => _recipe.DishTypes.Select(c => c.Type).ToList();
    
    private void StartDatabase(MyRecipeBookDbContext dbContext)
    {
        (_user, _password) = UserBuilder.Build();
        _recipe = RecipeBuilder.Build(_user);
        
        dbContext.Recipes.Add(_recipe);
        dbContext.Users.Add(_user);
        dbContext.SaveChanges();
    }
}