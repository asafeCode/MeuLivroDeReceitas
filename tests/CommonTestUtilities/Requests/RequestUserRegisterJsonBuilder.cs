using Bogus;
using MyRecipeBook.Communication.Requests;

namespace CommonTestUtilities.Requests;

public class RequestUserRegisterJsonBuilder
{
    public static RequestUserRegisterJson Build()
    {
        return new Faker<RequestUserRegisterJson>()
            .RuleFor(user => user.Name, (f) => f.Person.FirstName)
            .RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
            .RuleFor(user => user.Password, (f) => f.Internet.Password());
    }
}