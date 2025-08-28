using Bogus;
using CommonTestUtilities.Cryptography;
using MyRecipeBook.Application.Services.Cryptography;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Domain.Entities;

namespace CommonTestUtilities.Entities;

public static class UserBuilder
{
    public static (User user, string password) Build()
    {
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var password = new Faker().Internet.Password();
        
        var user= new Faker<User>()
            .RuleFor(user => user.Id, () => 1)
            .RuleFor(user => user.Name, (f) => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
            .RuleFor(user => user.Password, (f) => passwordEncripter.Encrypt(password));
        
        return (user, password);
    }
}